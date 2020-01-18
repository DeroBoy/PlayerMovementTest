using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float turnSpeed;
    public float currentMaxSpeed;
    public float speedPercent;
    public float sprintMultiplier;

    bool sprinting;

    Vector3 movement;

    Rigidbody playerRB;
    Animator anim;

    float h;
    float v;

    Vector3 previousFramePosition;
    Vector3 previousFrameRotation;

    public float moveSpeed;
    public float turnAngle;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        speedPercent = 0;
        previousFramePosition = transform.position;
        previousFrameRotation = new Vector3(h, 0, v);
        moveSpeed = 0;
        currentMaxSpeed = maxSpeed;
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Sprint();
    }

    void FixedUpdate()
    {
        Turn(h, v);
        Move(h, v);

        speedPercent = Mathf.Clamp01(Mathf.Sqrt((h * h) + (v * v)));
        if (speedPercent <= 0.25f && speedPercent > 0)
        {
            speedPercent = 0.25f;
        }
        if (sprinting)
        {
            speedPercent *= 1.4f;
        }
        anim.SetFloat("Speed", speedPercent);

            CalcSpeed();
            CalcTurnAngle();
    }

    void Move(float h, float v)
    {
        if (h != 0f || v != 0f)
        {
            movement.Set(h, 0, v);
            float inputSpeed = Mathf.Clamp01(Mathf.Sqrt((h * h) + (v * v)));
            if(inputSpeed <= 0.25f && inputSpeed > 0)
            {
                inputSpeed = 0.25f;
            }
            movement = transform.rotation * Vector3.forward * inputSpeed;
            movement = movement * currentMaxSpeed * Time.fixedDeltaTime;
            playerRB.MovePosition(transform.position + movement);
        }
    }
    void Turn(float h, float v)
    {
        Vector3 targetDirection = new Vector3(h, 0, v);
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            Quaternion newRotation = Quaternion.Slerp(playerRB.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
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

    void CalcSpeed()
    {
        if (h != 0f || v != 0f)
        {
            float movementPerFrame = Vector3.Distance(previousFramePosition, transform.position);
            moveSpeed = movementPerFrame / Time.fixedDeltaTime;
            previousFramePosition = transform.position;
        }
        else
        {
            moveSpeed = 0;
        }
    }

    void CalcTurnAngle()
    {
        if (h != 0f || v != 0f)
        {
            Vector3 currentInput = new Vector3(h, 0, v);
            if (currentInput != previousFrameRotation)
            {
                turnAngle = Vector3.Angle(currentInput, previousFrameRotation);
                if (turnAngle > 90)
                {
                    print("Sharp Turn Bro");
                }

                previousFrameRotation = currentInput;
            }
        }
        else
        {
            turnAngle = 0;
        }
    }   
}
