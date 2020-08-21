using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum summing_method { weighted_average, prioritized, dithered }

public enum behavior_type
{
    none = 0x00000,
    seek = 0x00002,
    flee = 0x00004,
    arrive = 0x00008,
    wander = 0x00010,
    cohesion = 0x00020,
    separation = 0x00040,
    allignment = 0x00080,
    obstacle_avoidance = 0x00100,
    wall_avoidance = 0x00200,
    follow_path = 0x00400,
    pursuit = 0x00800,
    evade = 0x01000,
    interpose = 0x02000,
    hide = 0x04000,
    flock = 0x08000,
    offset_pursuit = 0x10000,
};

public enum Deceleration
{
    slow = 3,
    normal = 2,
    fast = 1,
}

public class SteeringBehaviors
{
    public behavior_type m_iFlags;
    public summing_method m_SummingMethod = summing_method.prioritized;

    public Vehicle m_pVehicle;

    private float m_dWeightSeek = 1;
    private float m_dWeightFlee = 1;
    private float m_dWeightArrive = 1;

    public Vector2 m_vSteeringForce;
    public Deceleration m_Deceleration = Deceleration.normal;

    public Vector2 Calculate(Vector2 target)
    {
        switch (m_SummingMethod)
        {
            case summing_method.prioritized:
                return CalculatePrioritized(target);
        }
        return Vector2.zero;
    }

    Vector2 CalculatePrioritized(Vector2 target)
    {
        Vector2 force;

        if (On(behavior_type.seek))
        {
            force = Seek(target) * m_dWeightSeek;
            return force;
            if (!AccumulateForce(ref m_vSteeringForce, force))
                return m_vSteeringForce;
        }
        if (On(behavior_type.flee))
        {
            force = Flee(target) * m_dWeightFlee;
            return force;
            if (!AccumulateForce(ref m_vSteeringForce, force))
                return m_vSteeringForce;
        }
        if (On(behavior_type.arrive))
        {
            force = Arrive(target, m_Deceleration) * m_dWeightArrive;
            return force;
            if (!AccumulateForce(ref m_vSteeringForce, force))
                return m_vSteeringForce;
        }

        return m_vSteeringForce;
    }

    // 力计算的核心算法
    Vector2 Seek(Vector2 target)
    {
        Vector2 DesiredVelocity = (target - m_pVehicle.m_vPos).normalized * m_pVehicle.m_dMaxSpeed;
        return DesiredVelocity - m_pVehicle.m_vVelocity;
    }

    Vector2 Flee(Vector2 target)
    {
        float PanicDistanceSq = 2 * 2;
        if ((m_pVehicle.m_vPos - target).sqrMagnitude > PanicDistanceSq)
            return Vector2.zero;

        Vector2 DesiredVelocity = (m_pVehicle.m_vPos - target).normalized * m_pVehicle.m_dMaxSpeed;
        return DesiredVelocity - m_pVehicle.m_vVelocity;
    }

    Vector2 Arrive(Vector2 target, Deceleration deceleration)
    {
        Vector2 toTarget = target - m_pVehicle.m_vPos;
        float dist = toTarget.magnitude;
        if (dist > 0)
        {
            float DecelerationTweaker = 0.3f;
            float speed = dist / ((float) deceleration * DecelerationTweaker);
            speed = Mathf.Min(speed, m_pVehicle.m_dMaxSpeed);

            // 除以dist标准化向量
            Vector2 DesiredVelocity = toTarget * speed / dist;
            return DesiredVelocity - m_pVehicle.m_vVelocity;
        }
        return Vector2.zero;
    }

    bool On(behavior_type bt)
    {
        return (m_iFlags & bt) == bt;
    }

    bool AccumulateForce(ref Vector2 RunningTot, Vector2 ForceToAdd)
    {
        float MagnitudeSoFar = RunningTot.magnitude;
        float MagnitudeRemaining = m_pVehicle.m_dMaxForce - MagnitudeSoFar;

        // 不需要增加力
        if (MagnitudeRemaining <= 0)
            return false;

        float MagnitudeToAdd = ForceToAdd.magnitude;

        if (MagnitudeToAdd < MagnitudeRemaining)
        {
            RunningTot += ForceToAdd;
        }
        else
        {
            RunningTot += ForceToAdd.normalized * MagnitudeRemaining;
        }
        return true;
    }


}
