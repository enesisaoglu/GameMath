using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{

    public Vector2 localCoordinate;
    public Vector2 worldCoordinate;

    private void OnDrawGizmos()
    {
        //Vector2 worldPos = LocalToWorld(localCoordinate);
        //Gizmos.DrawSphere(worldPos, 0.1f);

        //Update in the inspector...
        localCoordinate = WorldToLocal(worldCoordinate);
        Gizmos.DrawSphere(worldCoordinate, 0.1f);

    }
    // 3b
    Vector2 WorldToLocal(Vector2 world)
    {
        Vector2 rel = world - (Vector2)transform.position;
        float x = Vector2.Dot(rel, transform.right); // x-axis...
        float y = Vector2.Dot(rel, transform.up); // y-axis...
        return new(x,y);
    }
    // 3a
    Vector2 LocalToWorld(Vector2 local)
    {
        Vector2 position = transform.position; //Origin...
        position += local.x * (Vector2)transform.right; // x-axis.
        position += local.y * (Vector2)transform.up; // y-axis.
        return position;
    }
}
