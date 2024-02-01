using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path 
{
    public readonly Vector3[] lookPoints;
    public readonly Line[] turnBoundries;
    public readonly int finishLineIndex;

    public Path(Vector3[] waypoints, Vector3 start, float turnDst)
    {
        lookPoints = waypoints;
        turnBoundries = new Line[lookPoints.Length];
        finishLineIndex = turnBoundries.Length-1;

        Vector2 previousPoint = V3TV2(start);

        for(int i = 0 ; i < lookPoints.Length; i++)
        {
            Vector2 currentPoint = V3TV2(lookPoints[i]);
            Vector2 direction = (currentPoint - previousPoint).normalized;
            Vector2 turnBoundry = (i == finishLineIndex)?currentPoint : currentPoint - direction * turnDst;
            turnBoundries[i] = new Line(turnBoundry,previousPoint - direction * turnDst);
            previousPoint = turnBoundry;
        }
    }

    Vector2 V3TV2 (Vector3 v3)
    {
        return new Vector2(v3.x ,v3.y); // maybe change .z to .y
    }

    
}
