using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUIQuit : MonoBehaviour
{
    public GameObject parent;

    void Awake()
    {
        Button bttn = this.GetComponent<Button>();
        bttn.onClick.AddListener(QuitApplication);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.C))
        {
            parent.SetActive(false);
        }
    }

    private void QuitApplication()
    {
        Application.Quit();
    }
}
