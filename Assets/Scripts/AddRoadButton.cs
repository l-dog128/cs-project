using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using UnityEditor;
public class AddRoadButton : MonoBehaviour
{
    public InputFieldButton inputFieldButton;
    public Material sharedMaterial;
    [HideInInspector]
    RoadManager roadManager;
    List<RoadData> roads;
    void Start()
    {
        //get refrence to RoadManger and set roads
        roadManager = Camera.main.GetComponent<RoadManager>();
        roads = roadManager.roads;
        
    }

    void Update()
    {
        if((!inputFieldButton.IsSavingOrLoading) && Input.GetKeyUp(KeyCode.N))
        {
            var Road = CreateNewRoad();
        }
    }
    //Method to create a new road when N is pressed
    public GameObject CreateNewRoad()
    {
        //Create new object, name it add road component  
        GameObject Road = new GameObject();
        Road.name = "Road" + roads.Count.ToString();
        Road.AddComponent<Road>();
    
        //Get Road component and set values 
        Road addedRoad = Road.GetComponent<Road>();
        addedRoad.material = sharedMaterial;
        addedRoad.roadData = new RoadData();

        //Add components and set layer
        Road.AddComponent<PathCreator>();
        Road.AddComponent<PathPlacer>();
        Road.AddComponent<RoadMeshCreator>();
        Road.AddComponent<MeshCollider>();
        Road.layer = 8;
        
        

        // doesnt configure settings as that is done is Road script
        roads.Add(addedRoad.roadData);
        Selection.activeGameObject = Road;

        //if more than one road deselects the previous road so no more points can be added to it
        if(roads.Count > 1)
            {
                roads[roads.Count-2].isSelected = false;
            }
        
        return Road;
    }

    //Method to create a new road when create new road on hub is clicked
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

    //Creates new road using the loaded data 
    public GameObject CreateNewRoad(RoadData loadedRoadData)
    {
        GameObject Road = new GameObject();
        Road.name = "Road" + roads.Count.ToString();
        Road.AddComponent<Road>();
        
        Road addedRoad = Road.GetComponent<Road>();
        addedRoad.material = sharedMaterial;
        addedRoad.roadData = new RoadData(loadedRoadData);
        addedRoad.roadData.isSelected = true;

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
}
