
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerManager : MonoBehaviour
{
    //new queue to hold all current car requests 
    Queue<CarRequest> carRequestsQueue = new Queue<CarRequest>();
    //current car request
    CarRequest currentCarRequest;
    static CarSpawnerManager instance;
    //used to check if spawining a car 
    bool isSpawningCar;
    //refrence to list of hubs in road manager 
    List<Hub> hubs;

    //creates a new array for differnt types of cars
    //car type made of name,shape,maxspeed and acceleration 
    CarType[] types = new CarType[]
    {
        new CarType("car",PrimitiveType.Sphere,0.5f,0.2f),
        new CarType("lorry",PrimitiveType.Cube,0.2f,0.1f),
        new CarType("bike",PrimitiveType.Capsule,0.5f,0.4f),
    };

    //set instance and hubs 
    void Awake()
    {
        instance = this;
        hubs = Camera.main.GetComponent<RoadManager>().hubs;
    }
    //method called by anything wanting to spawn a hub 
    //requires a starthub targethub and path that car will follow 
    public static void RequestSpawn(Hub startHub, Hub targetHub,Vector3[] path)
    {
        //create new request add to back of queue and try to process next item
        CarRequest newRequest = new CarRequest(startHub,targetHub,path);
        instance.carRequestsQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }
    //method that processes next item in the queue
    void TryProcessNext()
    {
        //checks if is currrently not spawing a car and and if the queuesize is 
        //greater than 0
        if(!isSpawningCar && carRequestsQueue.Count > 0)
        {
            //takes the first request from queue and removes it
            //set spawing car to true so no other requests can be made 
            currentCarRequest = carRequestsQueue.Dequeue();
            isSpawningCar = true;
            //creates a new car 
            currentCarRequest.startHub.CreateNewCar(currentCarRequest.endHub,types[UnityEngine.Random.Range(0,2)],currentCarRequest.waypoints);
        }
    }

    //method that is called after spawning a car 
    //bool success used to determine if car spawned properly
    public void FinishedSpawningCar(bool success)
    {
        //if successful try to process next item in queue 
        if(success)
        {
            isSpawningCar = false;
            TryProcessNext();
        }
    }


    //struct for a car request
    //startHub hub where car starts 
    //endHub hub where car ends 
    //waypoints the path the car will follow
    struct CarRequest
    {
        public Hub startHub;
        public Hub endHub;
        public Vector3[] waypoints;

        //constuctor for struct
        public CarRequest(Hub startHub_ , Hub endHub_, Vector3[] waypoints_)
        {
            startHub = startHub_;
            endHub = endHub_;
            waypoints = waypoints_;

        } 
    }
}
