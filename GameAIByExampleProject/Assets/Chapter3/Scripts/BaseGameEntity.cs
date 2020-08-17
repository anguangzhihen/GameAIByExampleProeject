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

    Vector2 m_vScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
