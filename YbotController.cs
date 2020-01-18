using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YbotController : MonoBehaviour
{
    Animator anim;
    Rigidbody playerRB;
    Vector3 movement;

    public float maxSpeed;
    public float turnSpeed;
    public float currentSpeed;
    public float currentMaxSpeed;
    public float speedPercent;
    public float sprintMultiplier;

    private float h;
    private float v;

    public bool sprinting;

    Vector3 previousFramePosition;
    public float speed;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        speedPercent = 0;
        currentMaxSpeed = maxSpeed;

        previousFramePosition = transform.position;
        speed = 0;
    }

    
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Sprint();
    }

    void FixedUpdate()
    { 
        Move(h, v);  
        speedPercent = Mathf.Clamp01(Mathf.Sqrt((h * h) + (v * v)));
        if (sprinting)
        {
            speedPercent += 0.1f;
        }

        anim.SetFloat("Speed", speedPercent);
        CalcSpeed();
    }

    void Move(float h, float v)
    {
        if (h != 0f || v != 0f)
        {
            movement.Set(h * currentMaxSpeed * Time.deltaTime, 0f, v * currentMaxSpeed * Time.deltaTime);
            playerRB.MovePosition(transform.position + movement);

            Turn(h, v);
        }
    }

    void CalcSpeed()
    {
        float movementPerFrame = Vector3.Distance(previousFramePosition, transform.position);
        speed = movementPerFrame / Time.deltaTime;
        previousFramePosition = transform.position;
    }

    void Turn(float h, float v)
    {
        Vector3 targetDirection = new Vector3(h, 0, v);
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            Quaternion newRotation = Quaternion.Slerp(playerRB.rotation, targetRotation, turnSpeed * Time.deltaTime);
            playerRB.MoveRotation(newRotation);
        }
    }

    void Sprint()
    {
        if (Input.GetAxisRaw("Rtrigger") > 0)
        {
            sprinting = true;
            currentMaxSpeed = maxSpeed * sprintMultiplier;
        }
        else
        {
            sprinting = false;
            currentMaxSpeed = maxSpeed;
        }
    }
}

