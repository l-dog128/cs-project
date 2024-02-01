using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script that sets the camera size ready for the grid
public class CameraSettings : MonoBehaviour
{
    public Vector2 size;
    BoxCollider2D boxCollider2D;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        float height = 2f*cam.orthographicSize;
        size = new Vector2(height*cam.aspect,height);
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = size;
    }
}
