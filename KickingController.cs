using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickingController : MonoBehaviour
{
    GameObject player;
    public GameObject ball;
    public Transform kickPos;

    public float kickForce;
    public float kickAngle;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
}
