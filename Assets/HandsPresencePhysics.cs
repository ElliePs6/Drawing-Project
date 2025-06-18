using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandsPresencePhysics : MonoBehaviour
{

    public Transform target;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegre, out Vector3 rotationAxis);
        Vector3 rotationDiffrenceInDegree = angleInDegre * rotationAxis;

        rb.angularVelocity = (rotationDiffrenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);





    }
}