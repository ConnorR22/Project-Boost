using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource aSource;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] float rocketThrust = 1500f;
    [SerializeField] float rocketRotation = 1000f;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        aSource = GetComponent<AudioSource>();
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
        rigidBody.freezeRotation = true; // Freeze rotation to let the rocket instead be manually rotated
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rigidBody.freezeRotation = false; // Unfreezing the rocket so the physics system can take over again
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!aSource.isPlaying)
            {
                aSource.PlayOneShot(mainEngine);
                
            }

            rigidBody.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
        }
        else
        {
            aSource.Stop();
        }
    }
}
