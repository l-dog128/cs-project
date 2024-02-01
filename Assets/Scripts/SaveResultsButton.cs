using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveResultsButton : MonoBehaviour
{
    // Refrence to button
    public Button button;
    // Refrence to inputParentField
    public GameObject inputFieldParent;
    // Refrence to inputFieldButton
    public InputFieldButton inputFieldButton;
    // Add event listener
    void Start()
    {
        button.onClick.AddListener(click);
    }

    // Method so when clicked set parent active and set setting to save 
    void click()
    {
        inputFieldParent.SetActive(true);
        inputFieldButton.setting = false;

    }
}
