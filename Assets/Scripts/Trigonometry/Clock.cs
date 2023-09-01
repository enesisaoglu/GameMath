using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class Clock : MonoBehaviour
{

    [Range(0, 60)]
    public float secondsTest;

    [Range(0, 0.2f)]
    public float tickSizeSecMin = 0.05f;

    [Range(0, 0.2f)]
    public float tickSizeHours = 0.05f;

    public bool smoothSeconds;
  

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        Handles.DrawWireDisc(default, Vector3.forward, 1);

     

        // Ticks (minutes/seconds)
        for (int i = 0; i < 60; i++)
        {
            Vector2 dir = secOrMinToDirection(i);
            DrawTick(dir, tickSizeSecMin, 1);
        }

        // Ticks (hours)
        for (int i = 0; i < 12; i++)
        {
            Vector2 dir = HoursToDirection(i);
            DrawTick(dir, tickSizeHours, 3);
        }

        // Hands
        DateTime time = DateTime.Now;
        float seconds = time.Second;
        if (smoothSeconds)
            seconds += time.Millisecond / 1000f;
        DrawClockHand( secOrMinToDirection(seconds), 0.9f, 1, Color.red );
        DrawClockHand(secOrMinToDirection(time.Minute), 0.7f, 4, Color.white);
        DrawClockHand(HoursToDirection(time.Hour), 0.5f, 8, Color.white);
    }

    void DrawTick(Vector2 direction, float length, float thickness)
    {
        Handles.DrawLine(direction, direction * (1f - length), thickness);
    }

    void DrawClockHand(Vector2 direction, float length, float thickness, Color color)
    {
        using (new Handles.DrawingScope(color))
        Handles.DrawLine(default, direction * length, thickness);
    }

    Vector2 HoursToDirection(float hours)
    {
        return ValueToDirection(hours, 12);
    }

    Vector2 secOrMinToDirection(float secOrMin)
    {
        return ValueToDirection(secOrMin, 60);
    }

    Vector2 ValueToDirection(float value, float valueMax)
    {
         // Converting seconds, range 0 to 1 from range 0 to 60...
        float percent = value / valueMax; // 0-1 value representing a percent as fraction for how far seconds are from along the 0 to 60 range...
        return FractionToClockDirection(percent);
    }

    Vector2 FractionToClockDirection(float percent)
    {
        // Find angle as radians...
        float angleRad = (0.25f - percent) * MathFG.TAU;
        return MathFG.AngleToDirection(angleRad);
    }

   



}
