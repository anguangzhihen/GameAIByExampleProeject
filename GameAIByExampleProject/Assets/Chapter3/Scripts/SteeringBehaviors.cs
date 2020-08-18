using System.Collections;
using System.Collections.Generic;
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

    public Vector2 Calculate()
    {
        switch (m_SummingMethod)
        {
            case summing_method.prioritized:
                return CalculatePrioritized();
        }
        return Vector2.zero;
    }

    Vector2 CalculatePrioritized()
    {
        if (On(behavior_type.seek))
        {
            
        }

        return Vector2.zero;
    }

    bool On(behavior_type bt)
    {
        return (m_iFlags & bt) == bt;
    }


}
