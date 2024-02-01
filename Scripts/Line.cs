using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line 
{

    const float vertLineGradient = 10e5f;
    float gradient;
    float y_intercept;
    Vector2 pointOnLine_1;
    Vector2 pointOnLine_2;

    float gradientPerpendicular;
    bool approachSide;

    public Line(Vector2 pointOnLine , Vector2 pointPerpendicular)
    {
        float dx = pointOnLine.x - pointPerpendicular.x;
        float dy = pointOnLine.y - pointPerpendicular.y;

        if(dx == 0 )
        {
            gradientPerpendicular = vertLineGradient;
        }
        else
        {
            gradientPerpendicular = dy/dx;
        }

        if(gradientPerpendicular == 0)
        {
            gradient = vertLineGradient;
        }
        else
        {   
        gradient = -1 / gradientPerpendicular;
        }

        y_intercept = pointOnLine.y - gradient* pointOnLine.x;
        pointOnLine_1 = pointOnLine;
        pointOnLine_2 = pointOnLine + new Vector2(1,gradient);

        approachSide = false;
        approachSide = GetSide(pointPerpendicular);
    }

    bool GetSide(Vector2 point)
    {
        return(point.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_1.y) > (point.y - pointOnLine_1.y) *(pointOnLine_2.x - pointOnLine_1.x); 
    }


    public bool HasCrossedLine(Vector2 point)
    {
        return (GetSide(point) != approachSide);
    }
}
