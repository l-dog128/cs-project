using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoadHub : MonoBehaviour
{

    public Sprite square;
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.H))
        {
            GameObject createdHub = CreateNewHubMouse(Input.mousePosition);    

        }
    }

    public GameObject CreateNewHubMouse(Vector3 mousePos)
    {
        GameObject hub = new GameObject();
        hub.name = "Test hub";
        

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        hub.transform.position = worldPos;
        hub.transform.localScale = Vector3.one * 15.75f;
        

        hub.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = hub.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = square;

        hub.AddComponent<Hub>();
        Hub _hub = hub.GetComponent<Hub>();
        
        hub.AddComponent<BoxCollider>().isTrigger = true;
        hub.layer = 8;
        return hub;
    }
    public GameObject CreateNewHubPos(Vector2 pos)
    {
        GameObject hub = new GameObject();
        hub.name = "Test hub";
        
        hub.transform.position = pos;
        hub.transform.localScale = Vector3.one * 15.75f;
        

        hub.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = hub.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = square;

        hub.AddComponent<Hub>();
        Hub _hub = hub.GetComponent<Hub>();
        
        hub.AddComponent<BoxCollider>().isTrigger = true;
        hub.layer = 8;
        return hub;
    }

}

