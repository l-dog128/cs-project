using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class Mesh2D : ScriptableObject
{
    [System.Serializable]
    public class Vertex
    {
       public Vector2 points;
        public Vector2 normal;
        public float u;
        //vertex colour
        //data
    }


    public Vertex[] Vertices;
    public int[] lineIndcies;

    public int vertexCount => Vertices.Length;
    public int lineCount => lineIndcies.Length;

}
