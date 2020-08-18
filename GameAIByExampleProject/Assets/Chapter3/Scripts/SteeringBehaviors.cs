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

public class SteeringBehaviors
{
    public behavior_type m_iFlags;
    public summing_method m_SummingMethod = summing_method.prioritized;

    public Vehicle m_pVehicle;

    private float m_dWeightSeek = 1;

    public Vector2 m_vSteeringForce;

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
            if (!AccumulateForce(ref m_vSteeringForce, force))
                return m_vSteeringForce;
        }

        return m_vSteeringForce;
    }

    Vector2 Seek(Vector2 target)
    {
        Vector2 DesiredVelocity = (target - m_pVehicle.m_vPos).normalized * m_pVehicle.m_dMaxSpeed;
        return DesiredVelocity - m_pVehicle.m_vVelocity;
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
