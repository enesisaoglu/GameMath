using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public Transform turret;

    [Header("Mouse Look")]
    public float mouseSensitivity = 1;
    public float turretYawSensitivity = 6;
    [Header("Movement")]
    public float playerAccelerationMagnitude = 400f;
    public float drag = 1.6f;


    // Internal state...
    float pitchDegree; // Up and Down...
    float yawDegree; // Right and Left...
    float turretYawOffsetDegree;


    public void Awake()
    {
        Vector3 startEuler = transform.eulerAngles;
        pitchDegree = startEuler.x;
        yawDegree = startEuler.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    //Turret placer with Update...
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        UpdateMovement();
        UpdateMouseLook();
        UpdateTurretYawInput();
        PlaceTurret();
    }

    Vector3 vel, acc;

    void UpdateMovement()
    {
        void TestInput(KeyCode key, Vector3 dir)
        {
            if (Input.GetKey(key))
            {
                acc += dir;
            }
        }

        acc = Vector3.zero;
        TestInput(KeyCode.W, transform.forward);
        TestInput(KeyCode.S, -transform.forward);
        TestInput(KeyCode.A, -transform.right);
        TestInput(KeyCode.D, transform.right);
        TestInput(KeyCode.Space, Vector3.up);
        TestInput(KeyCode.LeftControl, Vector3.down);


        if (acc != Vector3.zero)
        {
            acc = acc.normalized * playerAccelerationMagnitude;
        }

        vel += acc * Time.deltaTime;
        float delta = Time.deltaTime;
        transform.position += vel * delta;
    }

    void FixedUpdate()
    {
        vel /= drag; // movement dampening...
    }


    void UpdateTurretYawInput()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        turretYawOffsetDegree += scrollDelta * turretYawSensitivity;
        turretYawOffsetDegree = Mathf.Clamp(turretYawOffsetDegree, -90, 90);
    }

    void UpdateMouseLook()
    {
        if(Cursor.lockState == CursorLockMode.None)
        {
            return;
        }


        float xDelta = Input.GetAxis("Mouse X");
        float yDelta = Input.GetAxis("Mouse Y");

        pitchDegree += -yDelta * mouseSensitivity;
        pitchDegree = Mathf.Clamp(pitchDegree, -90, 90);
        yawDegree   +=  xDelta * mouseSensitivity;

        transform.rotation = Quaternion.Euler( pitchDegree, yawDegree, 0);

    }

    void PlaceTurret()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            turret.position = hit.point;
            Vector3 yAxis = hit.normal;
            Vector3 zAxis = Vector3.Cross(transform.right, yAxis).normalized;

            //Gizmos.color = Color.green;
            //Gizmos.DrawRay(hit.point, yAxis);
            Debug.DrawRay(hit.point, yAxis, Color.green);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawRay(hit.point, zAxis);
            Debug.DrawRay(hit.point, zAxis, Color.blue);
            //Gizmos.color = Color.black;
            //Gizmos.DrawLine(ray.origin, hit.point);
            Debug.DrawRay(ray.origin, hit.point, Color.black);

            Quaternion offsetRotation = Quaternion.Euler(0, turretYawOffsetDegree, 0); 
            turret.rotation = Quaternion.LookRotation(zAxis, yAxis) * offsetRotation;
        }
    }


    //Turret placer with Gizmo...
    //void OnDrawGizmos()
    //{
    //    if (turret == null)
    //        return;

    //    Ray ray = new Ray(transform.position, transform.forward);
    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        turret.position = hit.point;
    //        // Way1...
    //        // Grahm-Schmidt Orthogonalization...
    //        //Vector3 yAxis = hit.normal;
    //        //Vector3 xAxis = Vector3.Cross(yAxis,ray.direction).normalized;
    //        //Vector3 zAxis = Vector3.Cross(xAxis, yAxis); // it is already normalized...

    //        //Gizmos.color = Color.red;
    //        //Gizmos.DrawRay(hit.point, xAxis);


    //        // Way2...
    //        Vector3 yAxis = hit.normal;
    //        Vector3 zAxis = Vector3.Cross(transform.right, yAxis).normalized;

    //        Gizmos.color = Color.green;
    //        Gizmos.DrawRay(hit.point, yAxis);

    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawRay(hit.point, zAxis);

    //        Gizmos.color = Color.black;
    //        Gizmos.DrawLine(ray.origin, hit.point);
            
    //        turret.rotation = Quaternion.LookRotation(zAxis, yAxis);
    //    }

   
}
