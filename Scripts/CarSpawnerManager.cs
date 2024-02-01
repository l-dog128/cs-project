
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerManager : MonoBehaviour
{
   
    Queue<CarRequest> carRequestsQueue = new Queue<CarRequest>();
    CarRequest currentCarRequest;
    static CarSpawnerManager instance;
    bool isSpawningCar;
    List<Hub> hubs;

    void Awake()
    {
        instance = this;
        hubs = Camera.main.GetComponent<RoadManager>().hubs;
    }

    public static void RequestSpawn(Hub startHub, Hub targetHub)
    {
        CarRequest newRequest = new CarRequest(startHub,targetHub);
        instance.carRequestsQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if(!isSpawningCar && carRequestsQueue.Count > 0)
        {
            currentCarRequest = carRequestsQueue.Dequeue();
            isSpawningCar = true;
            currentCarRequest.startHub.CreateNewCar(currentCarRequest.endHub);
        }
    }

    public void FinishedSpawningCar(bool success)
    {
        if(success)
        {
            isSpawningCar = false;
            TryProcessNext();
        }
    }



   struct CarRequest
   {
    public Hub startHub;
    public Hub endHub;

    public CarRequest(Hub startHub_ , Hub endHub_)
    {
        startHub = startHub_;
        endHub = endHub_;
    } 
   }
}
