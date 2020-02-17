using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public float myTimeScale = 1.0f;
    public GameObject target;
    public float launchForce = 10f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = myTimeScale; // allow for slowing time to see what's happening
        rb = GetComponent<Rigidbody>();

        FiringSolution fs = new FiringSolution();
        Nullable<Vector3> aimVector = fs.Calculate(transform.position, target.transform.position, launchForce, Physics.gravity);
        if (aimVector.HasValue)
        {
            rb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
