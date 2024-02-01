using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HubPathLink
{
    // Refrence to hub
    Hub hub;
    // Dictionary containing all the hubs as keys and the path 
    Dictionary<Hub,Vector3[]> IsPath;
    // Constuctor
    public HubPathLink(Hub _hub)
    {
        hub = _hub;
        IsPath = new Dictionary<Hub, Vector3[]>();
    }
    // Method that trys to request a path and adds it to IsPath if one is found 
    public void TryAddPath(Hub targetHub)
    {
        // 50% chance to check if already contains as a new better path may be found 
        if((UnityEngine.Random.Range(0,1) == 0) ||!IsPath.ContainsKey(targetHub))
        {
            // Request a new path
            PathRequestManager.RequestPath(hub.transform.position,targetHub.transform.position,OnPathFound);
        }
        // If path is found try to add to dictionary if cannot means already there so update key value pair
        void OnPathFound(Vector3[] points,bool pathSuccessful)
        {
            if(pathSuccessful)
            {
                if(!IsPath.TryAdd(targetHub,points))
                {
                    IsPath[targetHub] = points;
                }
                
            }
            else
            {
                IsPath.Remove(targetHub);
            }
        }

    } 

    // Method for is there a path between parameter hub and hub that called this
    public bool ConnectedPath(Hub hub)
    {
        return IsPath.ContainsKey(hub); 
    }

    //Method that returns a path between two hubs 
    public Vector3[] GetPathForHub(Hub hub)
    {
        return IsPath[hub];
    }

}
