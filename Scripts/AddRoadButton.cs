using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using UnityEditor;
using System;
public class AddRoadButton : MonoBehaviour
{
    public Material sharedMaterial;
    [HideInInspector]
    RoadManager roadManager;
    RoadData RoadData;
    List<RoadData> roads;
    void Start()
    {
        roadManager = Camera.main.GetComponent<RoadManager>();
        roads = roadManager.roads;
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.N))
        {
            var Road = CreateNewRoad();
        }
    }

    public GameObject CreateNewRoad()
    {
        GameObject Road = new GameObject();
        Road.name = "Road" + roads.Count.ToString();
        Road.AddComponent<Road>();
    
        Road addedRoad = Road.GetComponent<Road>();
        addedRoad.material = sharedMaterial;
        addedRoad.roadData = new RoadData();

        Road.AddComponent<PathCreator>();
        Road.AddComponent<PathPlacer>();
        Road.AddComponent<RoadMeshCreator>();
        Road.AddComponent<MeshCollider>();
        Road.layer = 8;
        
        

        // doesnt configure settings as that is done is Road script
        roads.Add(addedRoad.roadData);
        Selection.activeGameObject = Road;

        if(roads.Count > 1)
            {
                roads[roads.Count-2].isSelected = false;
            }
        
        return Road;
    }


    public GameObject CreateNewRoad(Hub hub)
    {
        GameObject Road = new GameObject();
        Road.name = "Road" + roads.Count.ToString();
        Road.AddComponent<Road>();
        
        Road addedRoad = Road.GetComponent<Road>();
        addedRoad.material = sharedMaterial;
        addedRoad.roadData = new RoadData(hub);

        Road.AddComponent<PathCreator>();
        Road.AddComponent<PathPlacer>();
        Road.AddComponent<RoadMeshCreator>();
        Road.AddComponent<MeshCollider>();
        
        

        // doesnt configure settings as that is done is Road script
        roads.Add(addedRoad.roadData);
        Selection.activeGameObject = Road;

        if(roads.Count > 1)
            {
                roads[roads.Count-2].isSelected = false;
            }
        
        return Road;
    }


    public GameObject CreateNewRoad(RoadData loadedRoadData)
    {
        GameObject Road = new GameObject();
        Road.name = "Road" + roads.Count.ToString();
        Road.AddComponent<Road>();
        
        Road addedRoad = Road.GetComponent<Road>();
        addedRoad.material = sharedMaterial;
        addedRoad.roadData = new RoadData(loadedRoadData);

        Road.AddComponent<PathCreator>();
        Road.AddComponent<PathPlacer>();
        Road.AddComponent<RoadMeshCreator>();
        Road.AddComponent<MeshCollider>();
        Road.layer = 8;
        
        

        // doesnt configure settings as that is done is Road script
        roads.Add(addedRoad.roadData);
        Selection.activeGameObject = Road;

        if(roads.Count > 1)
            {
                roads[roads.Count-2].isSelected = false;
            }
        
        return Road;
    }
    public RoadData GetRoadData(GameObject Road)
        {
            return Road.GetComponent<RoadData>();
        }

}
