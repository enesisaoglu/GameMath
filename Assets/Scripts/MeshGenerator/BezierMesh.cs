using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMesh : MonoBehaviour
{

    Mesh mesh;

    public Vector3 profilePointA = new Vector3(0, 0, -1);
    public Vector3 profilePointB = new Vector3(0, 0, +1);


    public int segmentCount;
    public int vertexCount => 2 * (segmentCount + 1);

    private void OnValidate()
    {
        segmentCount = Mathf.Max(1, segmentCount); // Make sure it is always greater than 1...
    }

    List<Vector3> vertices = new List<Vector3>();
    List<Vector3> normals = new List<Vector3>();
    List<int> triangles = new List<int>();

    private void OnDrawGizmos()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.MarkDynamic();
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }
            
        Gizmos.matrix = transform.localToWorldMatrix;

        // Clear old data...
        vertices.Clear();
        normals.Clear();
        triangles.Clear();

        // Defining vertices...
        for (int i = 0; i < segmentCount + 1; i++)
        {
            float t = i / (float)segmentCount;
            Matrix4x4 bezM = GetBezierPoint(t);
            vertices.Add(bezM.MultiplyPoint3x4(profilePointA));
            vertices.Add(bezM.MultiplyPoint3x4(profilePointB));
            Vector3 normal = bezM.GetColumn(1);
            normals.Add(normal);
            normals.Add(normal);
        }

        // Defining triangles...
        for (int s = 0; s < segmentCount; s++)
        {
            int root = s * 2; // Finding the root(first) vertices of a triangle in segment...
            int neighbor = root + 1; // Neighboor vertices of root vertices...
            int next = root + 2; // Last vertices of a triangle in any segment...
            int nextNeighbor = root + 3; // neighbor vertices of last vertices of a triange...

            // First Triangle...
            triangles.Add(root);
            triangles.Add(neighbor);
            triangles.Add(next);

            // Second Triangle...
            triangles.Add(neighbor);
            triangles.Add(nextNeighbor);
            triangles.Add(next);
        }

        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetNormals(normals);
        mesh.SetTriangles(triangles, 0);
        

        //Gizmos.matrix *= bezM;
        //Gizmos.DrawSphere(Vector3.zero, 0.05f);
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(default, Vector3.right);
        //Gizmos.color = Color.green;
        //Gizmos.DrawRay(default, Vector3.up);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawRay(default, Vector3.forward);



    }

    Vector3 P0 => transform.GetChild(0).localPosition;
    Vector3 P1 => transform.GetChild(1).localPosition;
    Vector3 P2 => transform.GetChild(2).localPosition;
    Vector3 P3 => transform.GetChild(3).localPosition;


    private Matrix4x4 GetBezierPoint(float t)
    {
        Vector3 a = Vector3.Lerp(P0, P1, t);
        Vector3 b = Vector3.Lerp(P1, P2, t);
        Vector3 c = Vector3.Lerp(P2, P3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 origin = Vector3.Lerp(d, e, t); // Position...
        Vector3 tangent = (e - d).normalized; // Tangent to the curve...
        Vector3 normal = Vector3.zero; 
        //Swizzling...
        normal.x = -tangent.y; 
        normal.y = tangent.x;

        return new Matrix4x4(
            tangent, // X-Direction...
            normal, // Y-Direction...
            Vector3.forward, // Z-Direction...
            ToVector4(origin,1)// Position...
            );
    }

    private Vector4 ToVector4(Vector3 v3, float w = 0) => new Vector4(v3.x,v3.y,v3.z,w);
   
}
