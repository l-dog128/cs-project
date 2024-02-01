using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scrollbar : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI sliText;
    float value;
    Hub parentHub;
    // Start is called before the first frame update
    void Start()
    {
        parentHub = GetComponentInParent<Hub>();
        slider = GetComponent<Slider>();
        sliText = GameObject.Find("CPS Value").GetComponent<TextMeshProUGUI>();
        slider.onValueChanged.AddListener((v) => {
            sliText.text = v.ToString("0.00");
            parentHub.SetCpsValue(v);
        });

    }
}
