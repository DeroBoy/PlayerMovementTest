using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camTarget;
    public float offSet;
    public float height;
    public float camSmoothing;

    Vector3 targetPos;

    private Vector3 velocity = Vector3.zero;
    //RL4 cam offset = 20, height = 12, x-angle = 30
    //R18 cam offset = 18, height = 12.5, x-angle = 28
    //My far cam offset = 22, height = 17.5, x-angle = 35
    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        targetPos = new Vector3(camTarget.position.x, height, camTarget.position.z - offSet);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSmoothing);
    }
}
