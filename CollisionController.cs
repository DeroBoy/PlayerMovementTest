using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    void Start()
    {
        SetRigidBodyState(true);
        SetColliderState(false);
        GetComponent<Animator>().enabled = true;
    }

    /*private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            SetRigidBodyState(false);
            SetColliderState(true);

            GameObject.Find("mixamorig:Spine1").GetComponent<Rigidbody>().AddForce(transform.forward * 10000);
        }
    }*/

    void SetRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        Collider[] colls = GetComponents<Collider>();
        foreach(Collider coll in colls)
        {
            coll.enabled = !state;
        }
    }
}
