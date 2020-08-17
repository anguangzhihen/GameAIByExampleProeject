using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : BaseGameEntity
{
    private Vector2 m_vVelocity;
    private Vector2 m_vHeading;
    private Vector2 m_vSide;

    double m_dMass;
    double m_dMaxSpeed;
    double m_dMaxForce;
    double m_dMaxTurnRate;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
