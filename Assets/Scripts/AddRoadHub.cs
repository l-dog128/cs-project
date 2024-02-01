using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoadHub : MonoBehaviour
{
    //refrence to sprite used 
    public Sprite square;
    //refrence to input field used in update method 
    public InputFieldButton inputFieldButton;

    //Update method Checks if user pressed H key and user is not currently 
    //saving or loading
    void Update()
    {
        if((!inputFieldButton.IsSavingOrLoading) && Input.GetKeyUp(KeyCode.H))
        {
            GameObject createdHub = CreateNewHubMouse(Input.mousePosition);    

        }
    }

    //Method to create a new hub 
    //Vector3 mousePos the posistion where the hub will be made 
    public GameObject CreateNewHubMouse(Vector3 mousePos)
    {
        //create hub and name it 
        GameObject hub = new GameObject();
        hub.name = "Test hub";
        
        //convert the mouse posistion to a point in the world 
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        //set the posistion of the Hub and scale it to size
        hub.transform.position = worldPos;
        hub.transform.localScale = Vector3.one * 15.75f;
        
        //add spiteRender and set the sprite as a square 
        hub.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = hub.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = square;

        //add hub component 
        hub.AddComponent<Hub>();
        Hub _hub = hub.GetComponent<Hub>();
        
        //set the layer
        hub.AddComponent<BoxCollider>().isTrigger = true;
        hub.layer = 8;
        return hub;
    }
    //The same method as before but used when loading in a hub as only 
    //the x and y coordinates are saved
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

