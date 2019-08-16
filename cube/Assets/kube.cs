using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class kube : MonoBehaviour {
    
	// Use this for initialization
    Mesh threeDeeSquare;
    void Start () {
        threeDeeSquare = new Mesh();
        threeDeeSquare.vertices =
            new Vector3[]{new Vector3(-1, -1, -1 ),
            new Vector3(1, -1, -1),
            new Vector3(-1, 1, -1),
            new Vector3(1, 1, -1),
            new Vector3(-1, -1, 1),
            new Vector3(1, -1, 1),
            new Vector3(-1, 1, 1),
            new Vector3(1, 1, 1)};
        threeDeeSquare.triangles = new int[] {
            2,1,0,
            1,2,3,
            2,0,4,
            6,2,4,
            3,2,7,
            7,2,6,
            5,6,4,
            5,7,6,
            1,3,7,
            1,7,5,
            4,0,5,
            5,0,1
            };
        
		GetComponent<MeshFilter>().mesh = threeDeeSquare;
	}

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        if (threeDeeSquare != null)
        {
            for(int i = 0; i < threeDeeSquare.vertices.Length; i++)
            {
                Handles.Label(threeDeeSquare.vertices[i], i.ToString());
            }
        }
    }
    void Update () {
		
	}
}
