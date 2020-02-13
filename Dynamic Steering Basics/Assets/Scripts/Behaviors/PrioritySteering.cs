using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySteering
{
    // should be a small number, effectively zero
    float epsilon = 0.1f;

    public BlendedSteering[] groups;

    public SteeringOutput getSteering()
    {
        SteeringOutput steering = new SteeringOutput();
        foreach (BlendedSteering group in groups)
        {
            steering = group.getSteering();

            // check if we're above the threshold, if so return
            if (steering.linear.magnitude > epsilon || Mathf.Abs(steering.angular) > epsilon)
            {
                //Debug.Log(group.behaviors[0].behavior);
                return steering;
            }
        }

        // if we get here, it means that no group had a large enough
        // acceleration, so return the small acceleration from the
        // final group
        return steering;
    }
}
