using UnityEditor;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    public float radius = 1f;
    public Transform player;


    private void OnDrawGizmos()
    {
        Vector3 center = transform.position;
        
        if (player == null)
            return;
        
        Vector3 playerPos = player.position;
        // The formula to get distance between two vectors...
        Gizmos.DrawLine(center, playerPos);

        Vector3 delta = center - playerPos;
        //Way 1;
        //float distance = Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y + delta.z * delta.z);
        //Way 2;
        //float distance = Vector3.Distance(center, playerPos);
        //Way 3;
        //float Distance = Mathf.Sqrt( Vector3.Dot(delta, delta) );
        
        //bool inside = distance <= radius;//

        // Squared length of delta... //It is distance squared rather than square root...
        //Way 4;
        //float sqrDistance = delta.sqrMagnitude;
        //Way 5;
        //float sqrDistance = delta.x * delta.x + delta.y * delta.y + delta.z * delta.z;
        // way 6;
        float sqrDistance = Vector3.Dot(delta, delta); // Squared length of delta...
        bool inside = sqrDistance <= radius * radius;

        
       
        Gizmos.color = inside ? Color.white : Color.red;
        Gizmos.DrawWireSphere(center, radius);
    }
}
