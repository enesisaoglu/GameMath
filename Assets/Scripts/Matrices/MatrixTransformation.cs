using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTransformation : MonoBehaviour
{

    public Vector3 localCoordinate;
    public Vector3 worldCoordinate;

    private void OnDrawGizmos()
    {

        Matrix4x4 localToWorldMtx = transform.localToWorldMatrix;
        Vector4 vector = default;

        Vector4 vec = localToWorldMtx * vector;

        // Transforming between local and world...
        // transform.TransformPoint()         //  M*(v.x , v.y, v.z, 1)
        // transform.InverseTransformPoint()  //  M^-1*(v.x , v.y, v.z, 1)
        // transform.TransformVector()        //  M*(v.x , v.y, v.z, 0)
        // transform.InverseTransformVector() // M^-1*(v.x, v.y, v.z, 0)
       


        //Vector2 worldPos = LocalToWorld(localCoordinate);
        //Gizmos.DrawSphere(worldPos, 0.1f);

        //Update in the inspector...
        localCoordinate = WorldToLocal(worldCoordinate);
        Gizmos.DrawSphere(worldCoordinate, 0.1f);

    }
    // 3b
    Vector3 WorldToLocal(Vector3 world)
    {
        //Matrix4x4 mtx = Matrix4x4.TRS(new Vector3(2, 5, 6), Quaternion.identity, Vector3.one);

        return transform.InverseTransformPoint(world);
        
        //return transform.worldToLocalMatrix.MultiplyPoint3x4(world);

        //return transform.worldToLocalMatrix * new Vector4(world.x, world.y, world.z, 1);

        // Manual way...
        /*
        Vector3 rel = world - transform.position;
        float x = Vector3.Dot(rel, transform.right); // x-axis...
        float y = Vector3.Dot(rel, transform.up); // y-axis...
        float z = Vector3.Dot(rel, transform.forward); // z-axis...
        return new(x,y);
        */
    }
    // 3a
    Vector3 LocalToWorld(Vector3 local)
    {
        //Easy way...
        return transform.TransformPoint(local);

        //return transform.localToWorldMatrix.MultiplyPoint3x4(local);

        //return transform.localToWorldMatrix * new Vector4 (local.x, local.y, local.z , 1); // 1 means it will be also transformed...


        // manual way...
        /*
        Vector3 position = transform.position; //Origin...
        position += local.x * transform.right; // x-axis...
        position += local.y * transform.up; // y-axis...
        position += local.z * transform.forward; // z-axis...
        return position;
        */
    }
}
