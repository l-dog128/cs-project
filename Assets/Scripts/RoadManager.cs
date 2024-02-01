using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class RoadManager : MonoBehaviour
{
    // Refrebce to pathCreators which make the roads 
    public List<PathCreator> pathCreators;
    // Refrence to all the roadData classes
    public List<RoadData> roads;
    // Refrence to all the points that user has clicked
    public List<Vector2> points;
    // Refrence to all the points that make up road 
    public List<Vector2> pointsList;
    // Refrence to all the hubs
    public List<Hub> hubs;
    // Refrence to a hub with a menu active 
    public Hub activeHub;
    public bool HubActive;

    [ContextMenu("road count")]
    void blah()
    {
        Debug.Log(roads.Count);
    }

    // Refrence to AddRoadButton and AddRoadHub
    private AddRoadButton addRoadButton;
    private AddRoadHub addHubButton;
    // Initializes addRoadbutton and addHubButton and creates new road
    void Awake()
    {
        Time.timeScale = 1f;
        addRoadButton = Camera.main.GetComponent<AddRoadButton>();
        addHubButton = Camera.main.GetComponent<AddRoadHub>();
        roads = new List<RoadData>();
    }
    void Update()
    { 
        int PointsBefore = points.Count;
        UpdatePoints();
    
    }
    // Method that updates the points list
    private void UpdatePoints()
    {
        if(roads.Count > 0)
        {
            points.Clear();
            // Check for hubs and add hub to points 
            if(hubs.Count != 0)
            {
                foreach(Hub hub in hubs)
                {
                    points.Add(hub.transform.position);
                }
            }
            // Add each segment of road
            for(int i = 0; i < roads.Count;i++)
            {
                //Change points 
                pointsList = roads[i].spacedPoints;
                if(pointsList != null)
                {
                    for(int j=0 ;j < pointsList.Count;j++)
                    {
                        points.Add(pointsList[j]);
                    }
                }
            }
        }
    }
    // Method to create a new hub
    public void CreateNewHub(HubData hubData)
    {
        addHubButton.CreateNewHubPos(new Vector3(hubData.posX,hubData.posY,0));
    }

    // Method to create a new road 
    public void CreateNewRoad(RoadData roadData)
    {
        GameObject newRoad = addRoadButton.CreateNewRoad(roadData);
    }

    //Method to Destory a new road 
    
}