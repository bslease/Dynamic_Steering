using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Kinematic
{
    Pursue myMoveType;
    LookWhereGoing myRotateType;

    // Start is called before the first frame update
    void Start()
    {
        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;

        myMoveType = new Pursue();
        myMoveType.character = this;
        myMoveType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        steeringUpdate.linear = myMoveType.getSteering().linear;
        base.Update();
    }
}
