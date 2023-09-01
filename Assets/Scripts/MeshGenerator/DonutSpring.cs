using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutSpring : MonoBehaviour
{
    public bool torusMode;
    public Gradient colorGradient;
    public Color colorStart;
    public Color colorEnd;
    [Range(0, 4)] public float height = 1.0f;
    [Range(0, 2)] public float radiusMinor = 1.0f;
    [Range(0, 2)] public float radiusMajor = 1.0f;
    [Range(0, 8)] public float turnCount = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        const int DETAIL = 256;
        Vector3 previous = GetSpringPoint(0);
        // Basically drawing a math function from zero to one... 
        for (int i = 0; i < DETAIL; i++)
        {
            float t = i / (DETAIL - 1f);
            
            Gizmos.color = colorGradient.Evaluate(t); //Color.LerpUnclamped(colorStart, colorEnd, t);
            Vector3 point = GetSpringPoint(t);
            Gizmos.DrawLine(previous, point);
            previous = point;
        }
    }

    private Vector3 GetSpringPoint(float t)
    {
        // An angle in radians...
        float coilAngle = t * MathFG.TAU * turnCount;

        Vector2 xzVector = MathFG.AngleToDirection(coilAngle) * radiusMinor;

        if(torusMode)
        {
            Vector3 pDir = MathFG.AngleToDirection(t * MathFG.TAU);
            Vector3 p = pDir * radiusMajor;
            return p + xzVector.x * pDir + new Vector3 (0, 0, xzVector.y);
        }
        else
        {
            return new Vector3(xzVector.x, xzVector.y, t * height);
        }
    }
}
