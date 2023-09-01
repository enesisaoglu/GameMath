using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WedgeTriggerUsingTrigo : MonoBehaviour
{
    public  WedgeTriggerUsingTrigo Instance { get; private set; }

    public Transform target;

    public float radius;
    public float height;
    [Range(0, 180)]
    public float fieldOfViewDegree = 45f;

    float FieldOfViewRadians => fieldOfViewDegree * Mathf.Deg2Rad;
    float AngularThresh => Mathf.Cos (FieldOfViewRadians / 2);


    void Awake()
    {
        Instance = this;
    }

    void OnDrawGizmos()
    {
        // makes gizmos and handles relative to this transform...
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        Gizmos.color = Handles.color = Contains(target.position) ? Color.red : Color.magenta;

        Vector3 top = new Vector3(0, height, 0);


        float projection = AngularThresh;
        float x = Mathf.Sqrt(1 - projection * projection);

        Vector3 vRight = new Vector3(x, 0, projection) * radius;
        Vector3 vLeft = new Vector3(-x, 0, projection) * radius;


        //Handles.DrawWireDisc(default, Vector3.up, radius);
        //Handles.DrawWireDisc(top, Vector3.up, radius);

        Handles.DrawWireArc(default, Vector3.up, vLeft, fieldOfViewDegree, radius);
        Handles.DrawWireArc(top, Vector3.up, vLeft, fieldOfViewDegree, radius);

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

    public bool Contains(Vector3 position)
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
        if (flatDirToTarget.z < AngularThresh)
            return false; // outside the angular wedge...

        // Cylindrical radial test...
        if (flatDistance > radius)
            return false; // outside the infinite cylinder...

        // We are inside...
        return true;

    }
}