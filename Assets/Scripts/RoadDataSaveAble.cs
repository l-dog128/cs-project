using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoadDataSaveAble 
{
    // Refrences to all of the data that will be saved about the road
    public bool isSelected; 
    public float[] pointsX;
    public float[] pointsY;
    public float timeSinceCreation; 
    public bool connectedHub;
    public float firstPointX;
    public float firstPointY;

    // Constructor takes in road data 
    public RoadDataSaveAble(RoadData roadData)
    {
        isSelected = roadData.isSelected;
        connectedHub = roadData.ConnectedHub;
        firstPointX = roadData.FirstPoint.x;
        firstPointY= roadData.FirstPoint.y;

        pointsX = roadData.ConvertVectorListToArray(roadData.points,true);
        pointsY = roadData.ConvertVectorListToArray(roadData.points,false);
    }
    // Method that converts RoadDataSaveAble To RoadData
    public RoadData RoadDataSaveAbleToRoadData(RoadDataSaveAble roadDataSaveAble)
    {
        RoadData roadData = new RoadData();
        roadData.isSelected = roadDataSaveAble.isSelected;
        roadData.points = roadData.CombineArrays(roadDataSaveAble.pointsX,roadDataSaveAble.pointsY);
        roadData.ConnectedHub = roadDataSaveAble.connectedHub;
        roadData.FirstPoint = new Vector2(roadDataSaveAble.firstPointX,roadDataSaveAble.firstPointY);
        
        return roadData;
    }
}
