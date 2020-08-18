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

    void Awake()
    {
        m_pSteering = new SteeringBehaviors();
        m_pSteering.m_iFlags = behavior_type.seek;
        m_pSteering.m_pVehicle = this;

        this.m_dMaxSpeed = 150;
        this.m_dMaxForce = 200;
        this.m_dMass = 1;
    }

    void Update()
    {
        float time_elapsed = Time.deltaTime;

        Vector3 mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 OldPos = m_vPos;
        Vector2 SteeringForce = m_pSteering.Calculate(new Vector2(worldPos.x, worldPos.y));

        Vector2 acceleration = SteeringForce / m_dMass;
        m_vVelocity += acceleration * time_elapsed;
        m_vVelocity = Vector2.ClampMagnitude(m_vVelocity, m_dMaxSpeed);

        if (m_vVelocity.sqrMagnitude > 0.00001)
        {
            m_vHeading = m_vVelocity.normalized;
            m_vSide = Vector2.Perpendicular(m_vHeading);
        }

        // TODO 割裂的方式

        if (isSmoothingOn())
        {
            
        }

    }

    public bool isSmoothingOn()
    {
        return m_bSmoothingOn;
    }
}
