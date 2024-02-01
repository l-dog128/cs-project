using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using System;
using System.IO; using System.Runtime.Serialization.Formatters.Binary;

public class RoadData 
{
    public bool isSelected ; 
    public List<Vector2> points;
    public List<Vector2> spacedPoints;
    public float timeSinceCreation; 
    public bool ConnectedHub = false;
    public Vector2 FirstPoint;


    public RoadData()
    {
        isSelected = true; 
        points = new List<Vector2>();
        spacedPoints = new List<Vector2>();
        timeSinceCreation = 0; 
        ConnectedHub = false;
    }

    public RoadData(Hub hub)
    {
        isSelected = true; 
        points = new List<Vector2>();
        spacedPoints = new List<Vector2>();
        timeSinceCreation = 0; 
        ConnectedHub = true;
        FirstPoint = hub.Posistion2D();
    }
   
    public RoadData(RoadData LoadedRoadData)
    {
        isSelected = LoadedRoadData.isSelected; 
        points = LoadedRoadData.points;
        spacedPoints = LoadedRoadData.spacedPoints;
        timeSinceCreation = LoadedRoadData.timeSinceCreation; 
        ConnectedHub = LoadedRoadData.ConnectedHub;
        FirstPoint = LoadedRoadData.FirstPoint;
    }
    
    public List<Vector2> CombineArrays(float[] pointsX,float[] pointsY)
    {
        List<Vector2> tempList = new List<Vector2>();
        for(int i = 0 ; i < pointsX.Length; i++)
        {
            tempList.Add(new Vector2(pointsX[i],pointsY[i]));
        }

        return tempList;
    }

    public float[] ConvertVectorListToArray(List<Vector2> vList,bool x)
    {
        List<float> tempList = new List<float>();
        if(x)
        {
            foreach (Vector2 item in vList)
            {
                tempList.Add(item.x);
            }
        }
        else
        {
            foreach (Vector2 item in vList)
            {
                tempList.Add(item.y);
            }
        }
        return tempList.ToArray();

    }
}

