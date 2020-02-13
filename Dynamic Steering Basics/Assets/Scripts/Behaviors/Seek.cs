using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehavior
{
    public Kinematic character;
    public GameObject target;

    float maxAcceleration = 100f;

    public bool flee = false;

    protected virtual Vector3 getTargetPosition()
    {
        return target.transform.position;
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // Get the direction to the target
        if (flee)
        {
            //result.linear = character.transform.position - target.transform.position;
            result.linear = character.transform.position - getTargetPosition();
        }
        else
        {
            //result.linear = target.transform.position - character.transform.position;
            result.linear = getTargetPosition() - character.transform.position;
        }

        // give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}
