using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathFG
{
    // Math utilities...
    public const float TAU = 6.28318530718f;

    public static Vector3 AngleToDirection ( float angleRadian)
    {
        return new Vector2(Mathf.Cos(angleRadian), Mathf.Sin(angleRadian));
    }

    public static float DirectionToAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.x, vector.y);
    }
}
