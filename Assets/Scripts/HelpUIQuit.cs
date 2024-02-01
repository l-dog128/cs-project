using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUIQuit : MonoBehaviour
{
    // Refrence to parent GameObject
    public GameObject parent;

    void Awake()
    {   
        // Get Button and add event listner 
        Button bttn = this.GetComponent<Button>();
        bttn.onClick.AddListener(QuitApplication);
    }

    // Check if C is pressed and makes it not active 
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.C))
        {
            parent.SetActive(false);
        }
    }
    // Method to quit the application
    private void QuitApplication()
    {
        Application.Quit();
    }
}
