using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WedgeTrigger : MonoBehaviour
{
    public Transform target;

    public float radius;
    public float height;
    [Range(-1,1)]
    public float angleThresh = 0.5f; // not an actual angle...

    // Way1...
    void OnDrawGizmos()
    {
        // makes gizmos and handles relative to this transform...
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        Gizmos.color = Handles.color = Contains(target.position) ? Color.red : Color.magenta;

        Vector3 top = new Vector3 (0, height, 0);

        Handles.DrawWireDisc(default, Vector3.up, radius);
        Handles.DrawWireDisc(top, Vector3.up, radius);

        float projection = angleThresh;
        float x = Mathf.Sqrt(1 - projection * projection);

        Vector3 vRight = new Vector3(x, 0, projection) * radius;
        Vector3 vLeft = new Vector3(-x, 0, projection) * radius;


        //Bottom...
        Gizmos.DrawRay(default, vRight);
        Gizmos.DrawRay(default, vLeft);
        //Top...
        Gizmos.DrawRay(top, vRight);
        Gizmos.DrawRay(top, vLeft);

        Gizmos.DrawLine(default, top);
        Gizmos.DrawLine(vLeft, top + vLeft);
        Gizmos.DrawLine(vRight, top + vRight);
    }

    public bool Contains ( Vector3 position)
    {
        Vector3 vecToTargetWorld = (position - transform.position);
        // inverse transform = world to local...
        Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);


        // Height position check...
        if (vecToTarget.y < 0 || vecToTarget.y > height)
            return false; // outside the height range...

        // angular check...
        Vector3 flatDirToTarget = vecToTarget;
        flatDirToTarget.y = 0;
        float flatDistance = flatDirToTarget.magnitude;
        flatDirToTarget /= flatDistance; // normalizes flatDirToTarget...
        //Gizmos.color = Color.black;
        //Gizmos.DrawLine (transform.position, flatDirToTarget);
        if (flatDirToTarget.z < angleThresh)
            return false; // outside the angular wedge...

        // Cylindrical radial test...
        if(flatDistance > radius)
            return false; // outside the infinite cylinder...

        // We are inside...
        return true;

    }

    // Way2...
    //void OnDrawGizmos()
    //{
    //    // makes gizmos relative to this transform...
    //    Gizmos.matrix = transform.localToWorldMatrix;


    //    Vector3 origin = transform.position;
    //    Vector3 up = transform.up;
    //    Vector3 right = transform.right;
    //    Vector3 forward = transform.forward;

    //    Vector3 top = origin + up * height; // multiplyin up normal vector with height value to set length...
    //    Handles.DrawWireDisc(origin, up, radius);
    //    Handles.DrawWireDisc(top, up, radius);

    //    float projection = angleThresh;
    //    float x = Mathf.Sqrt(1 - projection * projection);

    //    Vector3 vRight = (forward * projection + right * x) * radius;
    //    Vector3 vLeft = (forward * projection + right * (-x)) * radius;

    //    //Bottom...
    //    Gizmos.DrawRay(origin, vRight);
    //    Gizmos.DrawRay(origin, vLeft);
    //    //Top...
    //    Gizmos.DrawRay(top, vRight);
    //    Gizmos.DrawRay(top, vLeft);

    //    Gizmos.DrawLine(origin, top);
    //    Gizmos.DrawLine(origin + vLeft, top + vLeft);
    //    Gizmos.DrawLine(origin + vRight, top + vRight);
    //}


}
