using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleTesting : MonoBehaviour
{
    public Transform aTf;
    public Transform bTf;
    public Transform cTf;
    public Transform pointTransform;

    public Vector2 a;
    public Vector2 b;
    public Vector2 c;
    public Vector2 p;


    private void OnDrawGizmos()
    {
        a = aTf.position;
        b = bTf.position;
        c = cTf.position;
        p = pointTransform.position;

        Gizmos.DrawSphere(p, 0.05f);


        Gizmos.color = TriangleContains(a, b, c, p) ? Color.red : Color.white;
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, a);
    }

    //Wedge Product / Determinant / 2D Cross Product...
    public float WedgeProduct(Vector2 v1, Vector2 v2) => v1.x * v2.y - v1.y * v2.x;
    bool TriangleContains(Vector2 a, Vector2 b, Vector2 c, Vector2 point)
    {
        bool ab = GetSideSign(a, b, point);
        bool bc = GetSideSign(b, c, point);
        bool ca = GetSideSign(c, a, point);
        return (ab == bc && bc == ca);
    }

    bool GetSideSign(Vector2 v1, Vector2 v2, Vector2 point)
    {
        Vector2 sideVec = v2 - v1;
        Vector2 pointRelative2v1 = point - v1;

        float scalarProjection = WedgeProduct(sideVec, pointRelative2v1);
        return scalarProjection > 0;
    }

}
