using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public Vector3 DirFromAngle(float angleInDeg,bool globalAngle)
    {
        if(!globalAngle)
        {
            angleInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg*Mathf.Deg2Rad),Mathf.Cos(angleInDeg*Mathf.Deg2Rad),0);
    }
}
