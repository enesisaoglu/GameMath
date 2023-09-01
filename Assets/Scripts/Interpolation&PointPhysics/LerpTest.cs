using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    [Range(0f, 1f)]
    public float t = 0;
    [Header("Transform")]
    public Transform aTf;
    public Transform bTf;
    public Transform cTf;
    public Transform dTf;
    [Header("Color")]
    public Color colorA;
    public Color colorB;

    private void OnDrawGizmos()
    {
        // Lerping Vector;
        Vector3 a = aTf.position;
        Vector3 b = bTf.position;
        Vector3 c = cTf.position;
        Vector3 d = dTf.position;

        //Vector3 pos = Vector3.Lerp(a, b, t);
        ////Lerping Color...
        //Gizmos.color = Color.Lerp(colorA, colorB, t);

        
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, d);

        //Draw the curve...
        const int DETAIL = 32;
        Vector2 prev = a;
        for (int i = 1; i < DETAIL; i++)
        {
            float tDraw = i / (DETAIL - 1f);
            Vector3 p = GetBezierPt(a, b, c, d, tDraw);
            Gizmos.DrawLine(prev, p);
            prev = p;
        }
        Gizmos.DrawSphere(GetBezierPt(a,b,c,d,t), 0.05f);
        
    }


    Vector3 GetBezierPt(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);

    }
}
