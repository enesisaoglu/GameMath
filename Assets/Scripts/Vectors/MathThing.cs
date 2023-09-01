using UnityEngine;

public class MathThing : MonoBehaviour
{
    // Testing GameObjects in scene...
    public Transform A;
    public Transform B;
    // Magnitudes of them...
    public float aMag;
    public float bMag;
    // Scale Projection of them...
    public float scProj;
    public Vector2 vecProj;
    public Vector2 aNorm;
    public Vector2 a;
    public Vector2 b;

    // No need to start game. It will always draw in the scene view...
    private void OnDrawGizmos()
    {
        a = A.position;
        b = B.position;

        // The length of both vectors
        //Way 1:
        //aMag = Mathf.Sqrt(a.x * a.x + a.y * a.y);
        //bMag = Mathf.Sqrt(b.x * b.x + b.y * b.y);
        //Way 2:
        // quick and easy form with Mathf...
        aMag = a.magnitude;
        bMag = b.magnitude;


        // Draw Vectors in coordinate...
       
        Gizmos.color = Color.red;
        Gizmos.DrawLine(default, a);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(default, b);

        //Normalize A and draw it...
        //Way 1:
        //Vector2 aNorm = a / aLen(aMag);
        //Vector2 aNorm = new Vector2 (a.x / aMag, a.y / aMag);
        //Way 2:
        // quick and easy form with Mathf...
        aNorm = a.normalized;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(aNorm, 0.05f);

        // Scalar Projection...
        //Way 1:
        //scProj = (aNorm.x * b.x) + (aNorm.y * b.y);
        //Way 2:
        // quick and easy form with Mathf...
        scProj = Vector2.Dot(aNorm, b);

       

        // Vector Projection...
        vecProj = aNorm * scProj;

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(vecProj, 0.05f);
    }
}
