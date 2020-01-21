using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{
    // override Align's getTargetAngle to face the target instead of matching it's orientation
    public override float getTargetAngle()
    {
        // work out the direction to the target
        Vector3 direction = target.transform.position - character.transform.position;

        // TODO - add a check in case we're right on top of the target

        // derive the facing angle from the direction from me to target
        float targetAngle = Mathf.Atan2(direction.x, direction.z); // radians
        targetAngle *= Mathf.Rad2Deg;

        return targetAngle;
    }
}
