using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class pentagon : MonoBehaviour {
    
	// Use this for initialization
    Mesh pentdragon;
    float GoldenRatio = (1+Mathf.Sqrt(5))/2;
    void Start () {
        pentdragon = new Mesh();
        pentdragon.vertices =
            new Vector3[]{
           new Vector3(Mathf.Sin(Mathf.PI*2 * 1/5), 0, Mathf.Cos(Mathf.PI*2 * 1/5) ),
           new Vector3(Mathf.Sin(Mathf.PI*2 * 2/5), 0, Mathf.Cos(Mathf.PI*2 * 2/5) ),
           new Vector3(Mathf.Sin(Mathf.PI*2 * 3/5), 0, Mathf.Cos(Mathf.PI*2 * 3/5) ),
           new Vector3(Mathf.Sin(Mathf.PI*2 * 4/5), 0, Mathf.Cos(Mathf.PI*2 * 4/5) ),
           new Vector3(Mathf.Sin(Mathf.PI*2 * 5/5), 0, Mathf.Cos(Mathf.PI*2 * 5/5) )};
        pentdragon.triangles = new int[] {
            0,1,4,
            4,1,3,
            3,1,2
            };
        
		GetComponent<MeshFilter>().mesh = pentdragon;
	}

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        if (pentdragon != null)
        {
            for(int i = 0; i < pentdragon.vertices.Length; i++)
            {
                Handles.Label(pentdragon.vertices[i], i.ToString());
            }
        }
    }
    void Update () {
		
	}
}
