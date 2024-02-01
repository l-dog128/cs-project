using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button button;
    public Hub hub;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        hub = GetComponentInParent<Hub>(true);
    }

    void TaskOnClick()
    {
       hub.clicked = true;
        
    }
}
