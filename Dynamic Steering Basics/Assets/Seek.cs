using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek
{
    public Kinematic character;
    public GameObject target;

    float maxAcceleration = 100f;

    public bool flee = false;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        if (flee)
        {
            result.linear = character.transform.position - target.transform.position;
        }
        else
        {
            result.linear = target.transform.position - character.transform.position;
        }

        // give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}
