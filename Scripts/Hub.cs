using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Diagnostics;

public class Hub : MonoBehaviour
{

    /*
    start node 
    end node 
    ui on click 
    ui for settings 
        car denisty and direction density 


    */
    [Range(0,3)]
    public int orientation;
    public Color colour;
    public bool clicked = false;
    public bool closeUi = false;
    public float GlobalTurnDst=4f;
    public float GlobalTurnSpeed=10f;
    public AddRoadButton ABR;
    GameObject hubUi;
    GameObject UI;
    GameObject HelpUI;
    CarSpawnerManager carSpawnerManager;
    RoadManager RoadManager;
    HubPathLink hubPathLink;
    float CarsPer10;
    public int arrivedCars;
    SpriteRenderer spriteRenderer;
    float startTime;

    List<int> avgSpeed;

    
    
    



    void Awake()
    {
        RoadManager = Camera.main.GetComponent<RoadManager>();
        RoadManager.hubs.Add(this);
        //Camera.main.GetComponent<RoadManager>().hubs.Add(this);

        carSpawnerManager = Camera.main.GetComponent<CarSpawnerManager>();
        hubUi = Camera.main.GetComponent<materialHolder>().GetHubUi();
        ABR = Camera.main.GetComponent<AddRoadButton>();
        hubPathLink = new HubPathLink(this);
        HelpUI = Camera.main.GetComponent<materialHolder>().HelpUi;

        
        UI =Instantiate(hubUi,gameObject.transform.position,Quaternion.identity);
        UI.transform.SetParent(gameObject.transform);
        UI.SetActive(false);
        CarsPer10 = 2.5f;


        
        colour = GetComponent<SpriteRenderer>().color;
        
        if(colour == Color.white)
        {
            colour = Random.ColorHSV();
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = colour;
        } 

        avgSpeed = new List<int>();

        StartCoroutine(CheckAndSpawnCars());
        
    }




    void OnMouseUp()
    {
        HelpUI.SetActive(false);
        UI.SetActive(true);
        
    }

    void Update()
    {
        if(closeUi == true)
        {
            UI.SetActive(false);
            HelpUI.SetActive(true);
            closeUi = false;
        }
        if(clicked == true )
        {
            NewRoad();

            clicked = false;
        }
        
    }
    
  
    IEnumerator CheckAndSpawnCars()
    {
        List<Hub> mostRecentHubs = RoadManager.hubs;
        yield return new WaitForSeconds(10/(CarsPer10*2));
        while(true)
        {
            foreach(Hub hub in mostRecentHubs)
            {
                if(hub != this)
                {
                    hubPathLink.TryAddPath(hub);
                    if(hubPathLink.ConnectedPath(hub))
                    {
                        CarSpawnerManager.RequestSpawn(this,hub);
                        yield return new WaitForSeconds(5/(CarsPer10*2)); 
                    }
                }
            }
            yield return new WaitForSeconds(5/(CarsPer10*2));
        }
        
    }
    void PathFound(Vector3[] path , bool succsess)
    {
        UnityEngine.Debug.Log(succsess);
    }



  
    public Vector3[] ComparePathLengths(Vector3[] path1, Vector3[] path2)
    {
        float len1 = 0;
        float len2 = 0;

        //calucate length of path 
        for(int i = 1; i < path1.Length-1;i++)
        {
            len1 = len1 + Vector3.SqrMagnitude(path1[i]-path1[i-1]);
        }
        for(int i = 1; i < path2.Length-1;i++)
        {
            len2 = len2 + Vector3.SqrMagnitude(path2[i]-path2[i-1]);
        }

        if(len1 > len2)
        {
            return path2; 
        }
        else
        {
            return path1;
        }
    }

    public void CreateNewCar(Hub targethub)
    {
        GameObject car = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        car.name = "car";
        

        Unit unit = car.AddComponent<Unit>();
        unit.targetHub = targethub;
        unit.target = targethub.transform;
        unit.turnDst = GlobalTurnDst;
        unit.turnSpeed = GlobalTurnSpeed;


        car.transform.position = gameObject.transform.position;
        car.transform.SetParent(gameObject.transform);
        car.transform.localScale = Vector3.one * 0.1f;

        

        var renderer = car.GetComponent<Renderer>();
        renderer.material.SetColor("_Color",targethub.colour);
        renderer.material.SetColor("_EmissionColor",targethub.colour);
        renderer.receiveShadows = false;

        
        FieldOfView fieldOfView = car.AddComponent<FieldOfView>();
        fieldOfView.viewRadius = 10f;
        fieldOfView.viewAngle = 60f;
        
        
        carSpawnerManager.FinishedSpawningCar(true);

    }

    void NewRoad()
    {
        ABR.CreateNewRoad(this);
    }

    public Vector2 Posistion2D()
    {
        return new Vector2(gameObject.transform.position.x,gameObject.transform.position.y);
    }

    public void SetCpsValue(float value)
    {
        CarsPer10 = value;
    }

    public void SetStartTimeAndCarCount()
    {
        startTime = Time.time;
        arrivedCars = 0;
    }
    public void AddTargetSpeed(float speed)
    {
        avgSpeed.Add((int)speed);
    }
    public float GetAverageSpeed()
    {
        return(avgSpeed.Sum());
    }
}