using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MovingEntity
{
    public GameWorld m_pWorld {
        get { return GameWorld.Instance; }
    }

    private SteeringBehaviors m_pSteering;

    void Awake()
    {
        m_pSteering = new SteeringBehaviors();
        m_pSteering.m_iFlags = behavior_type.seek;
    }
}
