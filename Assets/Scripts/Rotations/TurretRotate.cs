using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
    public Transform target;
    public WedgeTriggerUsingTrigoandRotate trigger;
    public Transform gunTransform;
    public float smoothingFactor =1.0f;

    Quaternion targetRotation;

    void Update()
    {
        if ( trigger.Contains( target.position ))
        {
            // When the target is inside of trigger...

            // world space...
            Vector3 vecToTarget = target.position - gunTransform.position;
            targetRotation = Quaternion.LookRotation( vecToTarget , target.up);
        }
        else{}
        
        // Smoothing rotate toward target...
        gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation, targetRotation, smoothingFactor * Time.deltaTime);
    }
}
