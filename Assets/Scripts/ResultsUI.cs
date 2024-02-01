using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsUI : MonoBehaviour
{
    // Refrence to if is selected 
    public bool selected;
    // Refrence to thr results 
    object[] results;
    // Refrences to GameObjects that show the results
    GameObject[] resultsArray;
    void Start()
    {
        results = new object[4];
        resultsArray = GameObject.FindGameObjectsWithTag("Results");
        selected = false;
        if(selected == false)
        {
            gameObject.SetActive(false);
        }
    }

    // Checks if user presses button C
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.C))
        {
            this.CloseUI();
        }
    }
    // Method that gets results from simulate button
    public void RecieveResults(object[] _results)
    {
        results[0] = _results[2]; 
        results[1] = _results[1]; 
        results[2] = _results[0]; 
        results[3] = _results[3];
    }
    // Set values
    public void SetResults()
    {
        // Get values from results array
        var speedValue = resultsArray[0].GetComponent<TextMeshProUGUI>();
        var materialValue = resultsArray[1].GetComponent<TextMeshProUGUI>();
        var complexityValue = resultsArray[2].GetComponent<TextMeshProUGUI>();

        speedValue.text = results[0].ToString();
        materialValue.text = results[1].ToString() + " Tons";
        complexityValue.text = results[2].ToString();


        //calulate score speed/cost 
        var scoreValue = resultsArray[3].GetComponent<TextMeshProUGUI>();
        scoreValue.text = results[3].ToString();
    }
    // Method that shows UI
    public void ShowResults()
    {
        gameObject.SetActive(true);
    }

    // Method that closes when clicked 
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
