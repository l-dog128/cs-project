using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    // The input field 
    public GameObject inputFieldGameObject;

    // The inputfield button 
    public InputFieldButton inputFieldButton;
    // The simulation button
    public SimulateButton simulateButton;
    void Awake()
    {
        // Get a reference to the button component attached to this game object
        Button bttn = this.GetComponent<Button>();
        bttn.onClick.AddListener(click);
    }

    // This function will be called when the button is clicked
    private void click()
    {
        // checks if not  currenttly simulating 
        if(!simulateButton.isSimulating)
        {
            //sets the input field to active and sets the button to load mode 
            inputFieldGameObject.SetActive(true);
            inputFieldButton.setting = false;

            inputFieldButton.IsSavingOrLoading = true;    
        }
    }
}
