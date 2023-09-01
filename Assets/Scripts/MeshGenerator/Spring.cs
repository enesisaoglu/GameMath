using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public Gradient colorGradient;
    public Color colorStart;
    public Color colorEnd;
    [Range(0,4)] public float height = 1.0f;
    [Range(0,2)] public float radius = 1.0f;
    [Range(0,8)] public float turnCount = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        const int DETAIL = 256;
        Vector3 previous = GetSpringPoint(0);
        // Basically drawing a math function from zero to one... 
        for (int i = 0; i < DETAIL; i++)
        {
            float t = i / (DETAIL -1f);
            float tColor = Mathf.Repeat ( t + Time.time, 1 );
            Gizmos.color = colorGradient.Evaluate(tColor); //Color.LerpUnclamped(colorStart, colorEnd, t);
            Vector3 point = GetSpringPoint(t);
            Gizmos.DrawLine(previous, point);
            previous = point;
        }
    }

    private Vector3 GetSpringPoint(float t)
    {
        // An angle in radians...
        float angle = t * MathFG.TAU * turnCount;

        Vector2 xzVector = MathFG.AngleToDirection(angle) * radius;

        return new Vector3(xzVector.x, xzVector.y, t * height);
    }
}
