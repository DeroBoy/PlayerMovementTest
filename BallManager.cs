using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject carrier = null;
    public bool isCarried;
    public bool onGround;
    bool ballLoose;

    Rigidbody ballRB;
    MeshCollider meshColl;


    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        meshColl = GetComponent<MeshCollider>();
    }

   
    void Update()
    {
        if (isCarried)
        {
            meshColl.enabled = false;
            ballRB.Sleep();
        }
        else
        {
            meshColl.enabled = true;
            ballRB.WakeUp();
        }
    }


}
