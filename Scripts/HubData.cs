using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HubData 
{
    public float posX;
    public float posY;

    public HubData (Hub hub)
    {
        posX = hub.transform.position.x;
        posY = hub.transform.position.y;
        
    }

}
