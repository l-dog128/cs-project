using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulateButton : MonoBehaviour
{ 
    RoadManager roadManager;
    Results recentResult;
    ResultsUI resultsUI;
    void Awake()
    {
        Button bttn = this.GetComponent<Button>();
        bttn.onClick.AddListener(Clicked);
        roadManager = Camera.main.GetComponent<RoadManager>();
        recentResult = new Results();
        resultsUI = GameObject.Find("BackGroundPannel").GetComponent<ResultsUI>();
        
        
    }

    void Clicked()
    {
        Debug.Log("starting sim");
        StopCoroutine(Simuate());
        StartCoroutine(Simuate());
    }


    //set time in all hubs to zero and car count to zero done 
    private void SetTimesAndCounts()
    {
        foreach(Hub hub in roadManager.hubs)
        {
            hub.SetStartTimeAndCarCount();
        }
    }

    //actually do simulation 
    IEnumerator Simuate()
    {
        
        // set material used and time count
        float materialUsed = GetMaterialUsed();
        int complexity = GetComplexity(materialUsed);
        SetTimesAndCounts();
        // wait ten senconds then get all the data back
        Time.timeScale = 4f;
        Debug.Log("fu fey show BING CHILLING" + Time.timeScale);

        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        Debug.Log("poo" + Time.timeScale);

        //get arrived Cars and speed
        float speed =CalculateSpeed();

        // update the created road class 
        recentResult.ModifyResults(complexity,materialUsed,speed);

        // send results to ResultsUI and set the values
        resultsUI.RecieveResults(recentResult.GetResults());
        resultsUI.SetResults();
        resultsUI.ShowResults();
        

    } 


    //get complexity/material used from road manager done
    private float GetMaterialUsed()
    {
        float materialUsed = 0;
        foreach (Vector2 point in roadManager.points)
        {
            materialUsed = materialUsed + point.sqrMagnitude;
        }
        return materialUsed;

    }
    private int GetComplexity(float materialUsed)
    {
        return (int)(materialUsed/roadManager.points.Count);
    }
    //calculate average car speed 
    private float CalculateSpeed()
    {
        var total = 0f;
        foreach(Hub hub in roadManager.hubs)
        {
            total += hub.GetAverageSpeed();
        }
        return(total/roadManager.hubs.Count);
    }

}

public class Results 
{
    int _complexity;
    float _materialUsed;
    float _carSpeed;
    float _score;

    public Results()
    {
        _complexity = 0;
        _materialUsed = 0;
        _carSpeed = 0;
        _score = 0;
    }

    public void ModifyResults(int newComplexity, float newMaterialUsed, float newCarSpeed)
    {
        _complexity = newComplexity;
        _materialUsed = newMaterialUsed;
        _carSpeed = newCarSpeed;
        _score = _carSpeed/_materialUsed;
    }
    public object[] GetResults()
    {
        return new object[4]{_complexity,_materialUsed,_carSpeed,_score};
    } 

}
