using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrigTest : MonoBehaviour
{
    [Range(0, 360)]
    public float angleDegree = 0f;

    static Vector2 AngToDir(float angleRad) => new Vector2 ( Mathf.Cos(angleRad), Mathf.Sin(angleRad) );

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(Vector3.zero, Vector3.forward, 1);

        // One turn per second...
        //float angleTurns = (float)(EditorApplication.timeSinceStartup);
        //Debug.Log(angleTurns);
        //Vector2 v = AngToDir(angleTurns * Mathf.PI * 2);

        float angleRadians = Mathf.Deg2Rad * angleDegree;
        Vector2 v = AngToDir(angleRadians);

        Gizmos.DrawRay(default, v);
    }
}
