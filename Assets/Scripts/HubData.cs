using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Means data can be saved 
[System.Serializable]
// Class storing the data of a Hub
public class HubData 
{
    public float posX;
    public float posY;
    // Constuctor
    public HubData (Hub hub)
    {
        posX = hub.transform.position.x;
        posY = hub.transform.position.y;
        
    }

}
