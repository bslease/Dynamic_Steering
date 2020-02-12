using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Kinematic
{
    public GameObject myCohereTarget;
    BlendedSteering mySteering;
    //Separation myMoveType;
    //LookWhereGoing myRotateType;

    // Start is called before the first frame update
    void Start()
    {
        // Separate from other birds
        Separation separate = new Separation();
        separate.character = this;
        GameObject[] goBirds = GameObject.FindGameObjectsWithTag("bird");
        Kinematic[] kBirds = new Kinematic[goBirds.Length-1];
        int j = 0;
        for (int i=0; i<goBirds.Length-1; i++)
        {
            if (goBirds[i] == this)
            {
                continue;
            }
            kBirds[j++] = goBirds[i].GetComponent<Kinematic>();
        }
        separate.targets = kBirds;

        // Cohere to center of mass
        Arrive cohere = new Arrive();
        cohere.character = this;
        cohere.target = myCohereTarget;

        LookWhereGoing myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        //myRotateType.target = myTarget;

        mySteering = new BlendedSteering();
        mySteering.behaviors = new BehaviorAndWeight[3];
        mySteering.behaviors[0] = new BehaviorAndWeight();
        mySteering.behaviors[0].behavior = separate;
        mySteering.behaviors[0].weight = 3f;
        mySteering.behaviors[1] = new BehaviorAndWeight();
        mySteering.behaviors[1].behavior = cohere;
        mySteering.behaviors[1].weight = 0.5f;
        mySteering.behaviors[2] = new BehaviorAndWeight();
        mySteering.behaviors[2].behavior = myRotateType;
        mySteering.behaviors[2].weight = 1f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        //steeringUpdate.linear = myMoveType.getSteering().linear;
        //steeringUpdate.angular = myRotateType.getSteering().angular;
        steeringUpdate = mySteering.getSteering();
        base.Update();
    }
}
