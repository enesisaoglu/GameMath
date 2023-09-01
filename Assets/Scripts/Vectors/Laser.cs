using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    int maxBounces = 40;
    private void OnDrawGizmos()
    {
        Vector2 origin = transform.position;
        Vector2 direction = transform.right; // X-Axis

        Ray ray = new Ray(origin, direction);

        Gizmos.color = Color.red;
        //Gizmos.DrawRay(ray);
        Gizmos.DrawLine(origin, origin + direction);
        
        for (int i = 0; i < maxBounces; i++)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Draw sphere on hit point...
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(hit.point, 0.05f);

                // Draw normal of any surface that has been hit...
                Gizmos.color = Color.white;
                Gizmos.DrawLine(hit.point, hit.point + hit.normal);
                
                Gizmos.color = Color.red;
                //Vector2 reflected = Vector2.Reflect(ray.direction, hit.normal);
                Vector2 reflected = Reflect(ray.direction, hit.normal);
                Gizmos.DrawLine(hit.point, (Vector2)hit.point + reflected);


                ray.direction = reflected;
                ray.origin = hit.point;
            }
            else//infinite...
            {
                break;
            }
        }
    }

    private Vector2 Reflect(Vector2 inDir, Vector2 normal)
    {
        float proj = Vector2.Dot(inDir, normal);
        return inDir - 2 * proj * normal;
    }
}
