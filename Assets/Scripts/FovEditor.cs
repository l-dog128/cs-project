using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FieldOfView))]
public class FovEditor : Editor
{
   void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white; 
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);
 
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius );
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius );
        Handles.DrawWireArc(fov.transform.position,Vector3.forward,viewAngleB,fov.viewAngle,fov.viewRadius);
    }
}
