using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour
{
    // Declare a public field for the UI element that should be displayed when the button is clicked
    public GameObject HelpUI;
    public GameObject SavingAndLoadingInputField;

    void Awake()
    {
        // Get a reference to the button component attached to this game object
        Button bttn = this.GetComponent<Button>();
        // Set the HelpUI GameObject to be inactive initially
        HelpUI.SetActive(false);
        // Add a listener for the button's onClick event
        bttn.onClick.AddListener(click);
    }
    // This function will be called when the button is clicked
    private void click()
    {
        // Check if saving and loading is active if it is deactivate 
        if(SavingAndLoadingInputField.activeSelf)
        {
            SavingAndLoadingInputField.SetActive(false);
        }
        // Set the HelpUI GameObject to be active
        HelpUI.SetActive(true);
    }
}

