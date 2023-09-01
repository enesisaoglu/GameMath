using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class WedgeTriggerUsingTrigoandRotate : MonoBehaviour
{
    public enum Shape
    {
        Wedge,
        Spherical,
        Cone
    }
 
    public Transform target;

    public Shape shape;

    [FormerlySerializedAs("radius")]
    public float radiusOuter = 1;
    public float radiusInner = 1;

    public float height;
    [Range(0, 360)]
    public float fieldOfViewDegree = 45f;

    float FieldOfViewRadians => fieldOfViewDegree * Mathf.Deg2Rad;
    float AngularThresh => Mathf.Cos (FieldOfViewRadians / 2);

    void SetGizmoMatrix(Matrix4x4 matrix) => Gizmos.matrix = Handles.matrix = matrix;

    void OnDrawGizmos()
    {
        // makes gizmos and handles relative to this transform...
        SetGizmoMatrix( transform.localToWorldMatrix );

        Gizmos.color = Handles.color = Contains(target.position) ? Color.red : Color.magenta;
       
        switch(shape)
        {
            case Shape.Wedge: DrawWedgeGizmo();
                break;
            case Shape.Spherical: DrawSphereGizmo();
                 break;
            case Shape.Cone: DrawConeGizmo();
                break;

        }

    }

    Stack<Matrix4x4> matrices = new Stack<Matrix4x4>();
    void PushMatrix() => matrices.Push(Gizmos.matrix);
    void PopMatrix() => SetGizmoMatrix(matrices.Pop());




    private void DrawConeGizmo()
    {

        float projection = AngularThresh;
        float x = Mathf.Sqrt(1 - projection * projection);
        // Normalized...
        Vector3 vRightDir = new Vector3(x, 0, projection);
        Vector3 vLeftDir = new Vector3(-x, 0, projection);
        // Inner and outer radius...
        Vector3 vRightOuter = vRightDir * radiusOuter;
        Vector3 vLeftOuter = vLeftDir * radiusOuter;
        Vector3 vRightInner = vRightDir * radiusInner;
        Vector3 vLeftInner = vLeftDir * radiusInner;

        //Arcs...
        void DrawFlatEdge()
        {
            Handles.DrawWireArc(default, Vector3.up, vLeftOuter, fieldOfViewDegree, radiusOuter);
            Handles.DrawWireArc(default, Vector3.up, vLeftInner, fieldOfViewDegree, radiusInner);

            Gizmos.DrawLine(vRightInner, vRightOuter);
            Gizmos.DrawLine(vLeftInner, vLeftOuter);
        }
        DrawFlatEdge();
        PushMatrix(); // Saves the current matrix to the stack...
        SetGizmoMatrix( Gizmos.matrix * Matrix4x4.TRS(default, Quaternion.Euler(0,0,90), Vector3.one));
        DrawFlatEdge();
        PopMatrix();


        //Rings...
        void DrawRing(float turretRadius)
        {
            float halfOfAngle = FieldOfViewRadians / 2;
            float distance = turretRadius * Mathf.Cos(halfOfAngle);
            float radius = turretRadius * Mathf.Sin(halfOfAngle);


            Vector3 center = new Vector3(0, 0, distance);
            Handles.DrawWireDisc(center, Vector3.forward, radius); 
        }
        DrawRing(radiusOuter);
        DrawRing(radiusInner);
        
    }

    private void DrawSphereGizmo()
    {
        Gizmos.DrawWireSphere(default, radiusInner);
        Gizmos.DrawWireSphere(default, radiusOuter);


    }

    private void DrawWedgeGizmo()
    {
        Vector3 top = new Vector3(0, height, 0);


        float projection = AngularThresh;
        float x = Mathf.Sqrt(1 - projection * projection);

        // Normalized...
        Vector3 vRightDir = new Vector3(x, 0, projection);
        Vector3 vLeftDir = new Vector3(-x, 0, projection);
        // Inner and outer radius...
        Vector3 vRightOuter = vRightDir * radiusOuter;
        Vector3 vLeftOuter = vLeftDir * radiusOuter;
        Vector3 vRightInner = vRightDir * radiusInner;
        Vector3 vLeftInner = vLeftDir * radiusInner;

        //Forward arc...
        Handles.DrawWireArc(default, Vector3.up, vLeftOuter, fieldOfViewDegree, radiusOuter);
        Handles.DrawWireArc(top, Vector3.up, vLeftOuter, fieldOfViewDegree, radiusOuter);
        //Back arc...
        Handles.DrawWireArc(default, Vector3.up, vLeftInner, fieldOfViewDegree, radiusInner);
        Handles.DrawWireArc(top, Vector3.up, vLeftInner, fieldOfViewDegree, radiusInner);

        Gizmos.DrawLine(vRightInner, vRightOuter);
        Gizmos.DrawLine(vLeftInner, vLeftOuter);

        Gizmos.DrawLine(top + vRightInner, top + vRightOuter);
        Gizmos.DrawLine(top + vLeftInner, top + vLeftOuter);

        Gizmos.DrawLine(vLeftInner, top + vLeftInner);
        Gizmos.DrawLine(vRightInner, top + vRightInner);
        Gizmos.DrawLine(vLeftOuter, top + vLeftOuter);
        Gizmos.DrawLine(vRightOuter, top + vRightOuter);
    }

    public bool Contains(Vector3 position) =>
        shape switch
        {
            Shape.Wedge => WedgeContains(position),
            Shape.Spherical => SphereContains(position),
            Shape.Cone => ConeContains(position),
            _             => throw new InvalidEnumArgumentException()
        };

    static float AngleBetweenNormalized(Vector3 v1, Vector3 v2)
    {
        return Mathf.Acos(Mathf.Clamp(Vector3.Dot(v1, v2), -1,1));
    }

    public bool ConeContains(Vector3 position)
    {
        if (SphereContains(position) == false)
        {
            return false;
        }

        Vector3 dirToTarget = (position - transform.position).normalized;
        float angleRad = AngleBetweenNormalized(transform.forward, dirToTarget); 
        return angleRad < FieldOfViewRadians / 2;


        //return Vector3.Angle(transform.forward, dirToTarget) < fieldOfViewDegree / 2;

        //float projAngle = Vector3.Dot(transform.forward, dirToTarget);
        //return projAngle > AngularThresh;
    }
    public bool SphereContains(Vector3 position)
    {
        //Distance between this object and target object...
        float distance = Vector3.Distance(transform.position, position);
        return distance >= radiusInner && distance <= radiusOuter;
    }

    public bool WedgeContains(Vector3 position)
    {
        //Target...
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
        if (flatDistance > radiusOuter || flatDistance < radiusInner)
            return false; // outside the infinite cylinder...

        // We are inside...
        return true;
    }
}