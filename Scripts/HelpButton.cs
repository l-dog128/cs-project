using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour
{
    public GameObject HelpUI;

    void Awake()
    {
        Button bttn = this.GetComponent<Button>();
        HelpUI.SetActive(false);
        bttn.onClick.AddListener(click);
    }

    private void click()
    {
        HelpUI.SetActive(true);
    }

}
