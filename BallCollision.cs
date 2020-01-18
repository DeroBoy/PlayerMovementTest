using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{

    public GameObject ball;
    public GameObject ballHolder;
    BallManager ballManager;

    bool holdingBall;


    void Start()
    {
        //ball = GameObject.Find("Ball");
        ballManager = ball.GetComponent<BallManager>();
        //ballHolder = GameObject.Find("ballHolder");
    }

 
    void Update()
    {
        if (holdingBall)
        {
            ball.transform.position = ballHolder.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball") && ballManager.carrier == null && !Input.GetButtonDown("A"))
        {
            ballManager.carrier = gameObject;
            ballManager.isCarried = true;

            holdingBall = true;
        }
    }
}
