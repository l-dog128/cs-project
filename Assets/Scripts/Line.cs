using UnityEngine;

public struct Line 
{
    // Set vetical gradient really high as want 2d line
    const float vertLineGradient = 10e5f;
    // gradient of line
    float gradient;
    // y intercept
    float y_intercept;
    //two points which make up a line
    Vector2 pointOnLine_1;
    Vector2 pointOnLine_2;
    // the perpendicular gradient of the line 
    float gradientPerpendicular;
    // Whether above or below the line
    bool approachSide;

    // Constuctor for line
    public Line(Vector2 pointOnLine , Vector2 pointPerpendicular)
    {
        // calculate differnt in x and differnce in y 
        float dx = pointOnLine.x - pointPerpendicular.x;
        float dy = pointOnLine.y - pointPerpendicular.y;

        // as cannont divide by zero
        if(dx == 0 )
        {
            gradientPerpendicular = vertLineGradient;
        }
        else
        {
            gradientPerpendicular = dy/dx;
        }
        // a verticle line 
        if(gradientPerpendicular == 0)
        {
            gradient = vertLineGradient;
        }
        else
        {   
        gradient = -1 / gradientPerpendicular;
        }
        // calculate y intercept
        y_intercept = pointOnLine.y - gradient* pointOnLine.x;
        // set first point and second point using gradient 
        pointOnLine_1 = pointOnLine;
        pointOnLine_2 = pointOnLine + new Vector2(1,gradient);

        approachSide = false;
        approachSide = GetSide(pointPerpendicular);
    }
    // Method which returns which side of line is on 
    bool GetSide(Vector2 point)
    {
        return(point.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_1.y) > (point.y - pointOnLine_1.y) *(pointOnLine_2.x - pointOnLine_1.x); 
    }

    // Method which tells you if the point is on the other side of the line
    public bool HasCrossedLine(Vector2 point)
    {
        return (GetSide(point) != approachSide);
    }
}
