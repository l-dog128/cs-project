using UnityEngine;

public class Path 
{
    // Refrence to the points to look to 
    public readonly Vector3[] lookPoints;
    // Refrence to lines which make the turn boundires
    public readonly Line[] turnBoundries;
    // the finish line index point
    public readonly int finishLineIndex;
    // Constructor takes a path a start path and a turning distance 
    public Path(Vector3[] waypoints, Vector3 start, float turnDst)
    {
        lookPoints = waypoints;
        // Create new line array length of lookpoints length
        turnBoundries = new Line[lookPoints.Length];
        // set finish line as last index
        finishLineIndex = turnBoundries.Length-1;

        Vector2 previousPoint = V3TV2(start);

        // Loop through all points 
        for(int i = 0 ; i < lookPoints.Length; i++)
        {
            Vector2 currentPoint = V3TV2(lookPoints[i]);
            Vector2 direction = (currentPoint - previousPoint).normalized;
            // calculates boundry given if i is the last or not 
            Vector2 turnBoundry = (i == finishLineIndex)?currentPoint : currentPoint - direction * turnDst;
            // create a new line
            turnBoundries[i] = new Line(turnBoundry,previousPoint - direction * turnDst);
            previousPoint = turnBoundry;
        }
    }
    // method to convert a vector3 to vector2 
    Vector2 V3TV2 (Vector3 v3)
    {
        return new Vector2(v3.x ,v3.y); // maybe change .z to .y
    }
}
