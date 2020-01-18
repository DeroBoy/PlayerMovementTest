using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBot : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;
    public GameObject movePosInd;
    public GameObject movePosSprite;
    
    Animator anim;

    public float moveSpeed = 10;
    public float turnSpeed;
    float dist;

    public float speedPercent;

    Vector3 newPos;
    Vector3 previousFramePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        newPos = transform.position;
        previousFramePosition = transform.position;
        
        movePosSprite.GetComponent<Renderer>().enabled = false;
    }

    void FixedUpdate()
    {
        Move();
        SpeedPercent();
    }

    void Move()
    {       
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 5000) && hit.transform.gameObject.tag == "Ground")
            {
                newPos = hit.point;
                movePosInd.transform.position = newPos;
                movePosSprite.GetComponent<Renderer>().enabled = true;
            }
        }

        dist = Vector3.Distance(newPos, transform.position);
        if (dist >= 0.5f)
        {
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(newPos - transform.position), turnSpeed * Time.fixedDeltaTime);

            Vector3 movement = transform.forward * Time.fixedDeltaTime * moveSpeed;
            rb.MovePosition(transform.position + movement);
        }     
    }

    void SpeedPercent()
    {
        float movementPerFrame = Vector3.Distance(previousFramePosition, transform.position) / Time.fixedDeltaTime;
        speedPercent = Mathf.Clamp01(movementPerFrame);
        anim.SetFloat("Speed", speedPercent);

        previousFramePosition = transform.position;
    }
}
