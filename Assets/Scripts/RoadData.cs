using System.Collections.Generic;
using UnityEngine;

// Class that hold all of the data about a road 
public class RoadData 
{

    // Refrence to is road selected
    public bool isSelected; 
    // All of the user clicked points
    public List<Vector2> points;
    // All points that make up road 
    public List<Vector2> spacedPoints;
    // Is the road connected to a hub and if it is first point is the hubs location 
    public bool ConnectedHub = false;
    public Vector2 FirstPoint; 
    // Constructor / initializer for the class 
    public RoadData()
    {
        isSelected = true; 
        points = new List<Vector2>();
        spacedPoints = new List<Vector2>();
        ConnectedHub = false;
    }
    // Constructor if road is connected to a hub 
    public RoadData(Hub hub)
    {
        isSelected = true; 
        points = new List<Vector2>();
        spacedPoints = new List<Vector2>(); 
        ConnectedHub = true;
        FirstPoint = new Vector2(hub.transform.position.x,hub.transform.position.y);
    }
    // Constructor given loaded roadData
    public RoadData(RoadData LoadedRoadData)
    {
        isSelected = LoadedRoadData.isSelected; 
        points = LoadedRoadData.points;
        spacedPoints = LoadedRoadData.spacedPoints;
        ConnectedHub = LoadedRoadData.ConnectedHub;
        FirstPoint = LoadedRoadData.FirstPoint;
    }
    // Method that combines to float arrays to make a vector to list
    public List<Vector2> CombineArrays(float[] pointsX,float[] pointsY)
    {
        List<Vector2> tempList = new List<Vector2>();
        for(int i = 0 ; i < pointsX.Length; i++)
        {
            tempList.Add(new Vector2(pointsX[i],pointsY[i]));
        }

        return tempList;
    }
    // Method that converts a Vector list to and array 
    // x represents if should do X or Y part of Vector 
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

