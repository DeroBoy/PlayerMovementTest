using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorRotation : MonoBehaviour
{
    public float camY;

    void FixedUpdate()
    {
        camY = Camera.main.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(90, camY, 0);
    }
}
