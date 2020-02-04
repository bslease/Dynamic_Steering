using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoider : Kinematic
{
    CollisionAvoidance myMoveType;

    public Kinematic[] myTargets = new Kinematic[4];

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new CollisionAvoidance();
        myMoveType.character = this;
        myMoveType.targets = myTargets;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = myMoveType.getSteering();
        base.Update();
    }
}
