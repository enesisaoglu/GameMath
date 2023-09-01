using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    // Signed speed, per axis. M/S...
    Vector3 velocity;
    Vector3 acceleration;

    float playerAccelerationMagnitude = 400;
    float drag = 1.6f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, velocity);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, acceleration);
    }

    private void Update()
    {
        ////Setting frame rate...
        //Application.targetFrameRate = 10;


        //// Object will move 1 meter per frame in x-axis...
        //transform.position += new Vector3(speed, 0, 0);

        //// Object will move 1 meter per second in x-axis...
        //float delta = Time.deltaTime;
        //transform.position += velocity * delta;

        // Velocity will have value with keyboard...
       
        // Local Function...
        void TestInput(KeyCode key, Vector3 dir) 
        {
           if (Input.GetKey(key)) 
            {
              acceleration += dir;
            }
        }
        
        acceleration = Vector3.zero;
        TestInput(KeyCode.W, Vector3.up);
        TestInput(KeyCode.S, Vector3.down);
        TestInput(KeyCode.A, Vector3.left);
        TestInput(KeyCode.D, Vector3.right);


        if(acceleration != Vector3.zero)
        {
            acceleration = acceleration.normalized * playerAccelerationMagnitude;
        }

        velocity += acceleration * Time.deltaTime;
        float delta = Time.deltaTime;
        transform.position += velocity * delta;
    }

    private void FixedUpdate()
    {
        // Movement dampening...
        velocity /= drag;


    }
}
