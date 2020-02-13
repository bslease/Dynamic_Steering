using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehavior
{
    public Kinematic character;
    public float maxAcceleration = 1f;

    // A list of potential targets
    public Kinematic[] targets;

    // The collision radius of a character (assuming all characters
    // have the same radius here).
    float radius = .1f; // .1

    public override SteeringOutput getSteering()
    {
        // 1. Find the target that's closes to collision
        // Store the first collision time
        float shortestTime = float.PositiveInfinity;

        // store the target that collides then, and other data that we
        // will need and can avoid recaculating
        Kinematic firstTarget = null;
        float firstMinSeparation = float.PositiveInfinity;
        float firstDistance = float.PositiveInfinity;
        Vector3 firstRelativePos = Vector3.positiveInfinity;
        Vector3 firstRelativeVel = Vector3.zero;

        // Loop through each target
        Vector3 relativePos = Vector3.positiveInfinity;
        foreach (Kinematic target in targets)
        {
            // calculate the time to collision
            relativePos = target.transform.position - character.transform.position;
            // the next line is a bug in Millington
            // the fix is on the following line
            //Vector3 relativeVel = target.linearVelocity - character.linearVelocity;
            Vector3 relativeVel = character.linearVelocity - target.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            // this is timeToClosestApproach, not timeToCollision
            float timeToCollision = (Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed));

            // check if it is going to be a collision at all
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;
            if (minSeparation > 2*radius)
            {
                continue; 
            }

            // check if it is the shortest [soonest time]
            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                // store the time, target and other data
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        // 2. Calculate the steering
        // if we have no target, then exit
        if (firstTarget == null)
        {
            return null;
        }

        ///////////////////////////////////////////////////
        // Millington actually steers you *toward* the target
        // I have no idea why you'd want to do this
        ///////////////////////////////////////////////////

        // if we're going to hit exactly, or if we're already
        // colliding, then do the steering based on current position.
        //if (firstMinSeparation <= 0 || firstDistance < 2*radius)
        //{
        //    relativePos = firstTarget.transform.position - character.transform.position;
        //}
        //else // otherwise cacluate the future relative position
        //{
        //    relativePos = firstRelativePos + firstRelativeVel * shortestTime;
        //}

        // Avoid the target.
        //SteeringOutput result = new SteeringOutput();
        //relativePos.Normalize();
        //result.linear = relativePos * maxAcceleration;
        //result.angular = 0;
        //return result;

        ////////////////////////////////////////
        /// Let's see if we can do better
        ////////////////////////////////////////

        SteeringOutput result = new SteeringOutput();

        // check for a head-on collision
        float dotResult = Vector3.Dot(character.linearVelocity.normalized, firstTarget.linearVelocity.normalized);
        if (dotResult < -0.9) // && dotResult > -1.1)
        {
            // if we have an impending head-on collision. veer sideways
            result.linear = -firstTarget.transform.right;
        }
        else
        {
            // otherwise, steer to pass behind our moving target
            result.linear = -firstTarget.linearVelocity;
        }
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.angular = 0;
        return result;
    }
}
