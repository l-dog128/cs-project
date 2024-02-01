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
    // Refrence to colour of Hub
    public Color colour;
    // Refrence to if hub has been clicked 
    public bool clicked = false;
    // Refrence to if should close UI
    public bool closeUi = false;
    // Refrence to AddRoadButton Script
    public AddRoadButton ARB;
    // Refrence to all the units  
    public List<Unit> units;
    
    // Refrence to the UI
    GameObject UI;
    // Refrence to helpbutton UI
    GameObject HelpUI;
    // Refrence to CarSpawner
    CarSpawnerManager carSpawnerManager;
    // Refrence to RoadManager
    RoadManager RoadManager;
    // Refrence to hubpathLink
    HubPathLink hubPathLink;
    // How many cars hub should spawn per 10 seconds
    float CarsPer10;
    // Refrence to spriteRenderer
    SpriteRenderer spriteRenderer;

    // Method that sets and create things as needed     
    void Awake()
    {

        RoadManager = Camera.main.GetComponent<RoadManager>();
        RoadManager.hubs.Add(this);
        //Camera.main.GetComponent<RoadManager>().hubs.Add(this);


        carSpawnerManager = Camera.main.GetComponent<CarSpawnerManager>();
        ARB = Camera.main.GetComponent<AddRoadButton>();
        hubPathLink = new HubPathLink(this);
        HelpUI = Camera.main.GetComponent<materialHolder>().HelpUi;
        units = new List<Unit>();

        
        UI =Instantiate(Camera.main.GetComponent<materialHolder>().hubUi,gameObject.transform.position,Quaternion.identity);
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

        StartCoroutine(CheckAndSpawnCars());
        
    }

    // Method that checks if there is a menu open if so cloeses it 
    // then closes the helpUI
    // then sets this hubs UI visable and sets that a menu is open
    // and this is the hub with it open
    void OnMouseUp()
    {
        if (RoadManager.activeHub != null)
        {
            RoadManager.activeHub.UI.SetActive(false);
        }
        HelpUI.SetActive(false);
        UI.SetActive(true);
        RoadManager.HubActive = true;
        RoadManager.activeHub = this;
    }

    void Update()
    {
        // checks if close button is pressed and closes hub UI if true
        if(closeUi == true)
        {
            UI.SetActive(false);
            RoadManager.HubActive = false;
            HelpUI.SetActive(true);
            closeUi = false;
        }
        // checks if new road button has been clicked
        if(clicked == true )
        {
            NewRoad();

            clicked = false;
        }
        
    }
    
  
    IEnumerator CheckAndSpawnCars()
    {
        while(true)
        {
            yield return new WaitForSeconds(5/(CarsPer10));
            // Loops through every hub 
            foreach(Hub hub in RoadManager.hubs.ToList())
            {
                // Checks isnt this hub
                if(hub != this)
                {
                    // Trys to add a path connecting this hub and other hub
                    // if there is a path request a new spawn  
                    hubPathLink.TryAddPath(hub);
                    if(hubPathLink.ConnectedPath(hub))
                    {
                        CarSpawnerManager.RequestSpawn(this,hub,hubPathLink.GetPathForHub(hub));
                        yield return new WaitForSeconds(5/(CarsPer10)); 
                    }
                }
            }
            yield return new WaitForSeconds(5/(CarsPer10));
        }
        
    }
    // Method to create a new car 
    // takes in target hub type of car and path the car will take 
    public void CreateNewCar(Hub targethub,CarType type,Vector3[] waypoints)
    {
        // Creates new shape and names it 
        GameObject car = GameObject.CreatePrimitive(type._shape);
        car.name = "car";

        // Add unit componet and set target acceleration and speed and path
        Unit unit = car.AddComponent<Unit>();
        unit.targetHub = targethub;
        unit.accerleration = type._accerleration;
        unit.maxSpeed = type._maxSpeed;

        unit.waypoints = waypoints;

        // Set posistion and size
        car.transform.position = gameObject.transform.position;
        car.transform.SetParent(gameObject.transform);
        car.transform.localScale = Vector3.one * 0.1f;


        // Add this car to the target hub units list 
        targethub.units.Add(unit);
        
        // Add renderer and set colour to target hubs colour
        var renderer = car.GetComponent<Renderer>();
        renderer.material.SetColor("_Color",targethub.colour);
        renderer.material.SetColor("_EmissionColor",targethub.colour);
        renderer.receiveShadows = false;

        
        FieldOfView fieldOfView = car.AddComponent<FieldOfView>();
        fieldOfView.viewRadius = 10f;
        fieldOfView.viewAngle = 60f;
        
        // Tells car spawner manager that finished spawing car 
        carSpawnerManager.FinishedSpawningCar(true);

    }
    // Method to create a new road 
    void NewRoad()
    {
        ARB.CreateNewRoad(this);
    }
    // Method that sets CarsPer10 value
    public void SetCpsValue(float value)
    {
        CarsPer10 = value;
    }
    // Method that gets mean speed for all cars 
    public float GetAverageSpeed()
    {
        float total = 0;
        foreach(Unit car in units)
        {
            total +=car.GetCurrentSpeed();
            
        }
        if(units.Count == 0)
        {
            return 0f;
        }
        return total/units.Count;
    }
}
// Struct for the CarType 
public struct CarType
{
    string _name;
    public PrimitiveType _shape;
    public float _maxSpeed;
    public float _accerleration;
    // Constructor
    public CarType(string name , PrimitiveType shape ,float maxSpeed , float accerleration)
    {
        _name = name;
        _shape = shape;
        _maxSpeed = maxSpeed;
        _accerleration = accerleration;
    }
}