using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
[System.Serializable]
public class BackGroundData 
{

    //list of all things that are going to be saved 
    //hubs colour posistions 
    public List<PathCreator> pathCreators;
    public List<RoadData> roads;
    public List<Vector2> points;
    public RoadData currentRoad;
    public RoadData NotSelectRoad;
    public List<Vector2> pointsList;
    public List<Hub> hubs;

    //intial values 
    public BackGroundData()
    {
        pathCreators = new List<PathCreator>();
        roads = new List<RoadData>();
        points = new List<Vector2>();
        hubs = new List<Hub>();
    }


    
}
