using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialHolder : MonoBehaviour
{
    public Sprite car;
    public Material mat;
    public GameObject HelpUi;
    public GameObject hubUi;
    public Material GetMaterial()
    {
        return mat;
    }
    public GameObject GetHubUi()
    {
        return hubUi;
    }

    
}
