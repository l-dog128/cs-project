using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HubPathLink
{
    Hub hub;
    Dictionary<Hub,bool> IsPath;
    public HubPathLink(Hub _hub)
    {
        hub = _hub;
        IsPath = new Dictionary<Hub, bool>();
    }
    //add new path 
    public void TryAddPath(Hub targetHub)
    {
        if(!IsPath.ContainsKey(targetHub))
        {
            PathRequestManager.RequestPath(hub.transform.position,targetHub.transform.position,OnPathFound);
        }
        void OnPathFound(Vector3[] points,bool pathSuccessful)
        {
            if(pathSuccessful)
            {
                IsPath.Add(targetHub,pathSuccessful);
            }
            else
            {
                IsPath.Remove(targetHub);
            }
        }

    } 

    //Does it contain 
    public bool ConnectedPath(Hub hub)
    {
        return IsPath.ContainsKey(hub); 
    }

}
