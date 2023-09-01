using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [Range(0, 8)]
    public float launchSpeed = 1;
    public float drawDuration = 1;
    Vector3 Position => transform.position;
    Vector3 Velocity => transform.right * launchSpeed;
    public Vector3 acceleration => Physics.gravity;

    public Rigidbody rigidBody; //Test Object...

    // Making move Test object on the trajectory path...
    private void Awake()
    {
        rigidBody.position = Position;
        rigidBody.velocity = Velocity;
    }

    private void FixedUpdate()
    {
        rigidBody.position = GetPoint(Time.time);
        rigidBody.velocity = GetVelocity(Time.time);
    }
    // Drawing the trajectory path...

    List<Vector3> drawPoints = new List<Vector3>();
    private void OnDrawGizmos()
    {
        const int DETAIL = 80;
        drawPoints.Clear();
        // Goes 0 to 1...
        for (int i = 0; i < DETAIL; i++)
        {
            float t = i / (DETAIL - 1f);
            float time = t * drawDuration;
            drawPoints.Add(GetPoint(time));
        }
        // Draw Lines (connect the samples)...
        for (int i = 0; i < DETAIL - 1; i++)
        {
            Gizmos.DrawLine(drawPoints[i], drawPoints[i + 1]);

        }

    }

    // Trajectory Calculation...
    public Vector3 GetPoint(float time) => Position + Velocity * time + (acceleration / 2) * time * time;

    public Vector3 GetVelocity(float time) =>  Velocity + acceleration * time;

}
