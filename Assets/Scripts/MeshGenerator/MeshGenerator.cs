using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    [ContextMenu("Generate Mesh")] 
    void GenerateMesh()
    {
        

        if (mesh == null)
        {
            mesh = new Mesh(); // Unity Object...
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }


        List<Vector3> Vertices = new List<Vector3>()
        {
            new Vector3(-1f,0f,-1f),
            new Vector3(-1f,0f,1f),
            new Vector3(1f,0f,1f),
            new Vector3(1f,0f,-1f)
        };

        List<Vector3> Normals = new List<Vector3>()
        {
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
        };

        List<int> Triangles = new List<int>() { 3,0,1,3,1,2 };


        mesh.Clear();
        mesh.SetVertices(Vertices);
        mesh.SetTriangles(Triangles, 0);
        mesh.SetNormals(Normals);
        

    }
}
