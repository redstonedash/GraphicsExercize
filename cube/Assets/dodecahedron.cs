using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class dodecahedron : MonoBehaviour
{

    // Use this for initialization
    Mesh dodecadragon;
    float GoldenRatio = (1 + Mathf.Sqrt(5)) / 2;
    public Texture uvTexture;
    [SerializeField] public Transform[] points;
    void Start()
    {
        dodecadragon = new Mesh();
        dodecadragon.vertices =
            new Vector3[]{
            //orange
            new Vector3(-1, -1, -1 ),
            new Vector3(1, -1, -1),
            new Vector3(-1, 1, -1),
            new Vector3(1, 1, -1),
            new Vector3(-1, -1, 1),
            new Vector3(1, -1, 1),
            new Vector3(-1, 1, 1),
            new Vector3(1, 1, 1),
            //green
            new Vector3(0, GoldenRatio, 1/GoldenRatio),
            new Vector3(0, -GoldenRatio, 1/GoldenRatio),
            new Vector3(0, GoldenRatio, -1/GoldenRatio),
            new Vector3(0, -GoldenRatio, -1/GoldenRatio),
            //blue
            new Vector3(1/GoldenRatio, 0, GoldenRatio),
            new Vector3(-1/GoldenRatio, 0, GoldenRatio),
            new Vector3(1/GoldenRatio, 0, -GoldenRatio),
            new Vector3(-1/GoldenRatio, 0, -GoldenRatio),
            //pink
            new Vector3(GoldenRatio, 1/GoldenRatio, 0),
            new Vector3(-GoldenRatio, 1/GoldenRatio, 0),
            new Vector3(GoldenRatio, -1/GoldenRatio, 0),
            new Vector3(-GoldenRatio, -1/GoldenRatio, 0)
            };
        
        dodecadragon.triangles = new int[] {
            5,9,18,
            18,9,11,
            18,11,1,//face
            18,16,7,
            18,7,12,
            18,12,5,//face
            14,18,1,
            14,16,18,
            14,3,16,//face
            3,10,8,
            3,8,7,
            3,7,16,//face
            9,5,12,
            9,12,13,
            9,13,4,//face
            12,7,8,
            12,8,6,
            12,6,13,//face
            13,6,17,
            13,17,19,
            13,19,4,//face
            8,10,2,
            8,2,17,
            8,17,6,
            17,2,15,
            17,15,0,
            17,0,19,
            10,3,14,
            10,14,15,
            10,15,2,
            0,15,14,
            0,14,1,
            0,1,11,
            19,0,11,
            19,11,9,
            19,9,4
            };
        splitEdges();
        //Vector3[] newNormals = new Vector3[dodecadragon.vertexCount];
        //for(int i = 0; i<dodecadragon.triangles.Length/3; i++)
        //{
        //    Debug.Log(i);
        //    Vector3 normal = Vector3.Cross(dodecadragon.vertices[dodecadragon.triangles[i*3]] - dodecadragon.vertices[dodecadragon.triangles[i*3+1]], dodecadragon.vertices[dodecadragon.triangles[i * 3]] - dodecadragon.vertices[dodecadragon.triangles[i*3+2]]));
        //    newNormals[dodecadragon.triangles[i*3]] = normal;
        //    newNormals[dodecadragon.triangles[i*3+1]] = normal;
        //    newNormals[dodecadragon.triangles[i*3+2]] = normal;
        //}
        //dodecadragon.normals = newNormals;
        dodecadragon.RecalculateNormals();
        Vector2[] uvs = new Vector2[dodecadragon.vertexCount];
        for(int i = 0; i < dodecadragon.vertexCount/3; i++)
        {
            if (points!=null)
            {
                
                if (points[i] != null) {
                    uvs[dodecadragon.triangles[i]] =points[dodecadragon.triangles[i]].position/100;
                }
                else
                {
                    points[dodecadragon.triangles[i]] = new GameObject().transform;
                }
            }
        }
        dodecadragon.uv = uvs;
        GetComponent<MeshFilter>().mesh = dodecadragon;
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        
        if (dodecadragon != null)
        {
            for (int i = 0; i < dodecadragon.vertices.Length; i++)
            {
                if(dodecadragon.vertices[i] == Vector3.zero)
                { 
                    Debug.LogAssertionFormat(this, "Bad vertex at index {0}", i);
                }

                Handles.Label(dodecadragon.vertices[i], i.ToString());
                Gizmos.DrawGUITexture(new Rect(new Vector2(0,0),new Vector2(100, 100)),uvTexture);
                Gizmos.DrawRay(dodecadragon.vertices[i], dodecadragon.normals[i]);
                
            }
            if (points != null)
            {
                for (int i = 0; i < dodecadragon.triangles.Length/3; i++)
                {
                    if (points[i] != null) {
                        //print(points[dodecadragon.triangles[i * 3]].position);
                        Gizmos.DrawLine(points[dodecadragon.triangles[i*3]].position, points[dodecadragon.triangles[i * 3 +1]].position);
                        Gizmos.DrawLine(points[dodecadragon.triangles[i * 3 + 2]].position, points[dodecadragon.triangles[i * 3 + 1]].position);
                        Gizmos.DrawLine(points[dodecadragon.triangles[i * 3]].position, points[dodecadragon.triangles[i * 3 + 2]].position);
                    } else
                    {
                        points[i] = new GameObject().transform;
                    }

                }
            }
        }
    }
    void Update()
    {
        if(points == null || points.Length < dodecadragon.vertexCount)
        {
            print("REBUILDING ARRAY");
            points = new Transform[dodecadragon.vertexCount];
            for(int i = 0; i < dodecadragon.vertexCount; i++)
            {
                if (points[i] == null)
                {
                    points[i] = new GameObject().transform;
                }
            }
        }
        
        Vector2[] uvs = new Vector2[dodecadragon.vertexCount];
        for (int i = 0; i < dodecadragon.vertexCount / 3; i++)
        {
            if (points != null)
            {

                if (points[i] != null)
                {
                    uvs[dodecadragon.triangles[i]] = (points[dodecadragon.triangles[i]].position*new Vector2(1,-1)+new Vector2(0,1)) / 100;
                }
                else
                {
                    points[dodecadragon.triangles[i]] = new GameObject().transform;
                }
            }
        }
        
        dodecadragon.uv = uvs;
        GetComponent<MeshFilter>().mesh = dodecadragon;
        if (EditorApplication.isPlaying)
        {
            transform.RotateAround(new Vector3(1,1,1),Time.deltaTime);
        }
    }
    private void splitEdges()
    {
        Vector3[] oldVerticies = new Vector3[dodecadragon.vertexCount];
        for(int i = 0; i < dodecadragon.vertexCount;i++)
        {
            oldVerticies[i] = dodecadragon.vertices[i];
        }
        Vector3[] newVerticies = new Vector3[dodecadragon.triangles.Length];
        int[] newOneTriangles = new int[dodecadragon.triangles.Length];
        for (int j = 0; j < dodecadragon.triangles.Length; j++)
        {
            int oldVertexIndex = dodecadragon.triangles[j];

            newVerticies[j] = oldVerticies[oldVertexIndex];

            Debug.Assert(newVerticies[j] != Vector3.zero);
                
            newOneTriangles[j] = j;
        }
        dodecadragon.vertices = newVerticies;
        dodecadragon.triangles = newOneTriangles;
    }
}
