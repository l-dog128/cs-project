using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;


public class RoadManager : MonoBehaviour
{
    public List<PathCreator> pathCreators;
    public List<RoadData> roads;
    public List<Vector2> points;
    public RoadData currentRoad;
    public RoadData NotSelectRoad;
    public List<Vector2> pointsList;
    public List<Hub> hubs;




    private AddRoadButton addRoadButton;
    private AddRoadHub addHubButton;

    
    [ContextMenu("save hopefully")]
    void SaveRoad()
    {
        int x  = 0; 
        foreach(RoadData road in roads)
        {
            SaveAndLoadPart2.SaveData(road,x);
            x+=1;
        }
        Debug.Log(Application.persistentDataPath);
    }
    [ContextMenu("load Hopefully")]
    void LoadRoad()
    {   
        int x  = 0;
        while(true)
        {
            try
            {
                //load road 
                RoadDataData notchangedData = SaveAndLoadPart2.LoadData(x);
                notchangedData.GetPoints();

                //change rdd to rd
                RoadData loadedData = notchangedData.RoadDataDataToRoadData(notchangedData); 
                //create new road and add new points
                GameObject newRoad =addRoadButton.CreateNewRoad(loadedData); 
            }
            catch
            {
                break;
            }
            x += 1;
            
        }


    }

    [ContextMenu("save hubs")]
    void SaveHub()
    {
        int x = 0;
        foreach(Hub hub in hubs)
        {   
            SaveAndLoadPart2.SaveHubs(hub,x);
            x += 1; 
        }
        
    }

    [ContextMenu("load Hubs")]
    void LoadHub()
    {
        int x = 0;
        while(true)
        {
            try
            {
                HubData hubData = SaveAndLoadPart2.LoadHubs(x);
                addHubButton.CreateNewHubPos(new Vector3(hubData.posX,hubData.posY,0));
            }
            catch
            {
                break;
            }
            x += 1;
        }
    }
    [ContextMenu("save all")]
    void SaveAll()
    {
        SaveRoad();
        SaveHub();
    }

    [ContextMenu("load all")]
    void LoadAll()
    {
        LoadHub();
        LoadRoad();
    }


    void Awake()
    {
        Time.timeScale = 1f;
        addRoadButton = Camera.main.GetComponent<AddRoadButton>();
        addHubButton = Camera.main.GetComponent<AddRoadHub>();
        roads = new List<RoadData>();
    }
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit,100f))
            {
                if(hit.transform != null)
                {
                    int selectedRoadIndex = GetSelctedRoad();
                    if(selectedRoadIndex > -1)
                    {
                        currentRoad = roads[selectedRoadIndex];
                        if(currentRoad.timeSinceCreation > 0.01f)
                        {
                            NotSelectRoad = (hit.transform.gameObject.GetComponent<RoadData>());
                            CombineRoad(currentRoad,NotSelectRoad);
                            currentRoad.isSelected = false;
                            
                        }
                    }
                }
            }
        }
        int PointsBefore = points.Count;
        UpdatePoints();
    }

    private void UpdatePoints()
    {
        if(roads.Count > 0)
        {
            points.Clear();
            // check for hubs and add hub to points 
            if(hubs.Count != 0)
            {
                foreach(Hub hub in hubs)
                {
                    points.Add(hub.transform.position);
                }
            }
            // add each segment of road
            for(int i = 0; i < roads.Count;i++)
            {//change points 
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

    
    public int GetSelctedRoad()
    {
        for(int i = 0 ; i < roads.Count ;i ++)
        {
            if(roads[i].isSelected == true)
            {
                return i;
            }
        }
        return -1;
    }   
    
    private void CombineRoad(RoadData selectedRoad , RoadData notSelectedRoad )
    {
        Vector2 point = selectedRoad.points[selectedRoad.points.Count-1];
        if(notSelectedRoad == null)
        {
            notSelectedRoad = selectedRoad;
        }
        int index = FindPosInList(notSelectedRoad.points, point);
        notSelectedRoad.points[index] = point;

    }

    private int FindPosInList(List<Vector2> list, Vector2 point)
    {
        var index = 0;
        var differnce = 100f;
        for(int i =0; i < list.Count;i++)
        {
            var currentDiff = Vector2.SqrMagnitude(list[i] - point );
            if (currentDiff < differnce)
            {
                differnce = currentDiff;
                index = i;
            }
        }
        return index;
    }
    
}