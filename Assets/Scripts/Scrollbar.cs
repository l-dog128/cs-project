using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Scrollbar for hub
public class Scrollbar : MonoBehaviour
{
    // Refrence to slider
    public Slider slider;
    // Refrencee to slider text
    public TextMeshProUGUI sliText;
    // Refrence to parent hub
    Hub parentHub;
    void Start()
    {
        // Gets hub and slider 
        parentHub = GetComponentInParent<Hub>();
        slider = GetComponent<Slider>();
        sliText = GameObject.Find("CPS Value").GetComponent<TextMeshProUGUI>();
        // When value is changed change hub Cars per 10 value
        slider.onValueChanged.AddListener((v) => {
            sliText.text = v.ToString("0.00");
            parentHub.SetCpsValue(v);
        });

    }
}
