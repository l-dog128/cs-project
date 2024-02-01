using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// New road button for the hub 
public class Buttons : MonoBehaviour
{
    // Refrence to button   
    public Button button;
    // Refrence to Hub
    public Hub hub;
    // Get Button hub and add event listener
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        hub = GetComponentInParent<Hub>(true);
    }
    // Method that when button clicked set hub clicked to true
    void TaskOnClick()
    {
       hub.clicked = true;
        
    }
}
