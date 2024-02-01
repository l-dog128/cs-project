using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    // Reference to the Hub component in the parent GameObject
    Hub hub;
    // Reference to the Button component on this GameObject
    Button button;
    void Start()
    {
        // Get the Button component on this GameObject
        button = GetComponent<Button>();
        // Add a listener to the button's onClick event
        button.onClick.AddListener(TaskOnClick);
        // Get the Hub component in the parent GameObject
        hub = GetComponentInParent<Hub>(true);
    }

    // This method is called when the button is clicked
    void TaskOnClick()
    {
        Debug.Log("clicked");
        // Set the closeUi property of the Hub component to true
        hub.closeUi = true;
    }
}

