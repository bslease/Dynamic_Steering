using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : Seek
{
    public GameObject[] path;

    // Millington p. 77: The distance along the path to generate the target. Can be
    // negative if the character is moving in the reverse direction.
    // Note: using GameObjects as waypoints reduces the need for this
    float pathOffset;

    // The current position on the path
    // float currentParam; // Millington's "param" is too ambiguous for me
    int currentPathIndex;

    // the distance at which we will consider ourselves to have reached the target
    float targetRadius = .5f; 

    public override SteeringOutput getSteering()
    {
        // Millington:
        //// 1. calculate the target to delegate to seek
        //// find the current position on the path
        //currentParam = path.getParam(character.transform.position, currentPos);
        //// offset it
        //float targetParam = currentParam + pathOffset;
        //// get the target position
        //target.position = path.getPosition(targetParam);

        // if we're just starting out we won't have an initial target
        // find the nearest waypoint and use that as our initial target
        if (target == null)
        {
            int nearestPathIndex = 0;
            float distanceToNearest = float.PositiveInfinity;
            for (int i = 0; i < path.Length; i++)
            {
                GameObject candidate = path[i];
                Vector3 vectorToCandidate = candidate.transform.position - character.transform.position;
                float distanceToCandidate = vectorToCandidate.magnitude;
                if (distanceToCandidate < distanceToNearest)
                {
                    nearestPathIndex = i;
                    distanceToNearest = distanceToCandidate;
                }
            }

            target = path[nearestPathIndex];
        }

        // if we've reached a waypoint, target the next
        float distanceToTarget = (target.transform.position - character.transform.position).magnitude;
        if (distanceToTarget < targetRadius)
        {
            currentPathIndex++;
            if (currentPathIndex > path.Length - 1)
            {
                currentPathIndex = 0;
            }
            target = path[currentPathIndex];
        }

        // delegate to seek
        return base.getSteering();
    }
}
