using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoadDataData 
{
    public bool isSelected; 
    public float[] pointsX;
    public float[] pointsY;
    public float timeSinceCreation; 
    public bool connectedHub;
    public float firstPointX;
    public float firstPointY;

    public RoadDataData(RoadData roadData)
    {
        isSelected = roadData.isSelected;
        timeSinceCreation = roadData.timeSinceCreation;
        connectedHub = roadData.ConnectedHub;
        firstPointX = roadData.FirstPoint.x;
        firstPointY= roadData.FirstPoint.y;

        pointsX = roadData.ConvertVectorListToArray(roadData.points,true);
        pointsY = roadData.ConvertVectorListToArray(roadData.points,false);
    }
    public void GetPoints()
    {
        Debug.Log(pointsX.Length);
    }
    public RoadData RoadDataDataToRoadData(RoadDataData roadDataData)
    {
        RoadData roadData = new RoadData();
        roadData.isSelected = roadDataData.isSelected;
        roadData.points = roadData.CombineArrays(roadDataData.pointsX,roadDataData.pointsY);
        roadData.timeSinceCreation = roadDataData.timeSinceCreation;
        roadData.ConnectedHub = roadDataData.connectedHub;
        roadData.FirstPoint = new Vector2(roadDataData.firstPointX,roadDataData.firstPointY);
        
        return roadData;
    }
}
