using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : BaseGameEntity
{
    public Vector2 m_vVelocity;
    public Vector2 m_vHeading;
    public Vector2 m_vSide; // 垂直于m_vHeading的向量

    public float m_dMass;
    public float m_dMaxSpeed;
    public float m_dMaxForce;
    double m_dMaxTurnRate;
}
