using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rigidBody;
    [SerializeField] float rocketThrust = 150f;
    [SerializeField] float rocketRotation = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rocketRotation);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rocketRotation);
        }
    }

    void ApplyRotation(float rotation)
    {
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
        }
    }
}
