using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SimulateButton : MonoBehaviour
{ 
    // Refrence to Result struct
    public Results recentResult;
    // Currently simulaitng 
    public bool isSimulating;
    // Refrence to road manager
    RoadManager roadManager;
    // Refrence to UI
    ResultsUI resultsUI;

    // Method to set and create things that are needed 
    void Awake()
    {
        Button bttn = this.GetComponent<Button>();
        bttn.onClick.AddListener(Clicked);
        roadManager = Camera.main.GetComponent<RoadManager>();
        recentResult = new Results();
        resultsUI = GameObject.Find("BackGroundPannel").GetComponent<ResultsUI>();
        
        
    }
    // Method that ends current simulation and starts new one when button is clicked 
    void Clicked()
    {
        Debug.Log("starting sim");
        StopCoroutine(Simuate());
        StartCoroutine(Simuate());
    }

    // Method that does simulation 
    IEnumerator Simuate()
    {
        isSimulating = true;
        float[] speeds = new float[5];
        // Wait five senconds then get all the data back
        Time.timeScale = 4f;
        for(int i = 0 ; i < 5 ; i++)
        {
            float speedAtT = CalculateSpeed();
            speeds[i] = speedAtT;
            yield return new WaitForSecondsRealtime(1);
        }
        // Set material used and Get Complextiy
        float materialUsed = GetMaterialUsed();
        int complexity = GetComplexity(materialUsed);
        Time.timeScale = 1f;
        Debug.Log("poo" + Time.timeScale);

        // Update the created road class 
        float meanSpeed = speeds.Sum()/speeds.Length;
        recentResult.ModifyResults(complexity,materialUsed,meanSpeed*10);

        // Send results to ResultsUI and set the values
        resultsUI.RecieveResults(recentResult.GetResults());
        resultsUI.SetResults();
        resultsUI.ShowResults();

    } 


    // Get material used from road manager 
    private float GetMaterialUsed()
    {
        float materialUsed = 0;
        foreach (Vector2 point in roadManager.points)
        {
            materialUsed = materialUsed + point.sqrMagnitude;
        }
        return materialUsed/1000;

    }
    // Get Complexity used from road manager 
    private int GetComplexity(float materialUsed)
    {
        return (int)(materialUsed * 500/roadManager.points.Count);
    }
    // Calculate average car speed 
    private float CalculateSpeed()
    {
        var total = 0f;
        foreach(Hub hub in roadManager.hubs)
        {
            total += hub.GetAverageSpeed();
        }
        if(roadManager.hubs.Count == 0)
        {
            return 0f;
        }
        float final = total/roadManager.hubs.Count;
        return(final*10);
    }

}
// Struct for resuts
public class Results 
{
    int _complexity;
    float _materialUsed;
    float _carSpeed;
    float _score;
    
    // Constructor sets all as zero so can be made in Awake()
    public Results()
    {
        _complexity = 0;
        _materialUsed = 0;
        _carSpeed = 0;
        _score = 0;
    }
    // Method to modify the results
    public void ModifyResults(int newComplexity, float newMaterialUsed, float newCarSpeed)
    {
        _complexity = newComplexity;
        _materialUsed = newMaterialUsed;
        _carSpeed = newCarSpeed;
        _score = 100 * _carSpeed/_materialUsed;
    }
    // Method that results results as an object array
    public object[] GetResults()
    {
        return new object[4]{_complexity,_materialUsed,_carSpeed,_score};
    } 

}
