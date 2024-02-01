using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    Hub hub;
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        hub = GetComponentInParent<Hub>(true);
    }

    void TaskOnClick()
    {
        Debug.Log("clicked");
        hub.closeUi = true;
    }
}
