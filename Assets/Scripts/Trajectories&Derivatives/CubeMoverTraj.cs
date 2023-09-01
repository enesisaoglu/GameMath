using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoverTraj : MonoBehaviour
{

    public float velocity;
    public float acc;
    //public Vector3 velocityVec => new Vector3(velocity, 0, 0);
    Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, new Vector3(VelocityX(Time.time), 0, 0));

    }

    float PositionX (float time)
    {
        // To visualize, use desmos calculator...
        return -2*time + time * time;
    }

    float VelocityX(float time)
    {
        // To visualize, use desmos calculator...
        return -2 + 2 * time;
    }

    //Every Rendered Frame...
    private void Update()
    {
        transform.position += startPosition + new Vector3 ( PositionX(Time.time),0,0 );
    }   


}
