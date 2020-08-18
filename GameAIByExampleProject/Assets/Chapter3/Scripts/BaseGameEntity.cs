using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameEntity : MonoBehaviour
{
    private Vector2 m_vPos
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.y);
        }
        set { transform.position = value; }
    }

    public Vector2 m_vScale;
    public double m_dBoundingRadius;
}
