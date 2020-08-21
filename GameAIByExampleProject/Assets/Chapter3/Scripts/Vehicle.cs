using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MovingEntity
{
    public GameWorld m_pWorld {
        get { return GameWorld.Instance; }
    }

    private SteeringBehaviors m_pSteering;
    private bool m_bSmoothingOn = true;

    private Vector2 m_vSmoothedHeading;

    public behavior_type m_iFlags;

    void Awake()
    {
        m_pSteering = new SteeringBehaviors();
        m_pSteering.m_iFlags = m_iFlags;
        m_pSteering.m_pVehicle = this;

        this.m_dMaxSpeed = 1.5f;
        this.m_dMaxForce = 2;
        this.m_dMass = 1;
    }

    void Update()
    {
        float time_elapsed = Time.deltaTime;

        Vector3 mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 OldPos = m_vPos;
        // Calculate计算出来的实际上是一个需求的速度，我的理解是它默认除以1s并乘以Mass，获得了力
        Vector2 SteeringForce = m_pSteering.Calculate(new Vector2(worldPos.x, worldPos.y));

        Vector2 acceleration = SteeringForce / m_dMass;
        m_vVelocity += acceleration * time_elapsed;
        m_vVelocity = Vector2.ClampMagnitude(m_vVelocity, m_dMaxSpeed);

        // 更新位置
        m_vPos += m_vVelocity * time_elapsed;

        if (m_vVelocity.sqrMagnitude > 0.00001)
        {
            m_vHeading = m_vVelocity.normalized;
            m_vSide = Vector2.Perpendicular(m_vHeading);
        }

        // TODO 割裂的方式

        if (isSmoothingOn())
        {
            
        }


        Render();
    }

    void Render()
    {
        // 更新朝向
        var to = new Vector3(m_vHeading.x, m_vHeading.y, 0);
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, to);
    }

    public bool isSmoothingOn()
    {
        return m_bSmoothingOn;
    }
}
