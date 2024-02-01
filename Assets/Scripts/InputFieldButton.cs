using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PathCreation;
public class InputFieldButton : MonoBehaviour
{
    // The input field 
    public TMP_InputField inputField;
    //the button this is attached to
    public Button button;
    //The user input 
    private string userInput;
    //The parent GameObject 
    public GameObject parentGameObject;
    //determines if the UI can be seen on screen
    private bool isActive;
    //determines the setting false = save , true = load 
    public bool setting; 
    //the path that the folder will use 
    private string path;
    //the simulate button from which the results are gotten from
    public SimulateButton simulateButton;
    
    public string recentSavePath;
    public bool IsSavingOrLoading;
    void Start()
    {
        inputField.text = "Enter FileName";
        button.onClick.AddListener(Clicked);
        parentGameObject.SetActive(isActive);
        IsSavingOrLoading = false;
    }
    // Get the value in the input field and set it to blank
    // then call the method depending on the mode 
    void Clicked()
    {
        userInput = inputField.text;
        inputField.text = "Enter FileName";
        if(setting)
        {
            Load();
            
        }
        else
        {
            StartCoroutine(Save());
        }
    }
    
    [ContextMenu("check if work")]
    void check()
    {
        Debug.Log(recentSavePath);
    }
    private IEnumerator Save()
    {
        // Check the input is valid by making sure it has a valid ascii type
        foreach(char c in userInput)
        {
            if(! ((c >= 65 && c<=90)||(c >=97 && c <=122)))// if not(lower or upper) 
            {
                if(!(c >= 48 && c <= 57)) // isn't a number 
                {
                    inputField.text = "Invalid Input";
                    IsSavingOrLoading = false; 
                    parentGameObject.SetActive(false);
                    yield return null;
                }
            
            }
            
        }
        //Combine the input and application directory to create a path
        string path = System.IO.Path.Combine(Application.persistentDataPath,userInput);
        //checks if the path exsists if it does change name 
        if(!Directory.Exists(path))
        {
            DirectoryInfo folder = Directory.CreateDirectory(path);
            SaveData(path);
        }
        else
        {
            //checks path exsits and is is the same as the recent save path
            if(Directory.Exists(path) && (recentSavePath == path))
            {
                SaveData(recentSavePath);
            }
            else
            {
                inputField.text = "file already exsits";
            }
            
        }
        yield return new WaitForSecondsRealtime(2f);
        IsSavingOrLoading = false; 
        parentGameObject.SetActive(false);
        yield return null;
    }
    private void SaveData(string path)
    {
        //gets components from the road manager and saves them
        List<RoadData> roads = Camera.main.GetComponent<RoadManager>().roads;
        List<Hub> hubs = Camera.main.GetComponent<RoadManager>().hubs;
        //gets components from results if there is any 
        object[] recentResult = simulateButton.recentResult.GetResults();
        // loop through all of the hubs and roads saving them 
        int x = 0;
        int y = 0;
        foreach(RoadData road in roads)
        {
            SaveAndLoad.SaveData(road,x,path);
            x += 1;
        }
        foreach(Hub hub in hubs)
        {
            SaveAndLoad.SaveHubs(hub,y,path);
            y += 1;
        }
        //saves the results if not zero
        if(!((int)recentResult[0] == 0))
        {
            SaveAndLoad.SaveSimulationResults(path,recentResult);
        }
        //change the text to show has been saved
        recentSavePath = path;
        inputField.text = "Saved Succsessfully";
    }
    private void Load()
    {
        //Combine the input and application directory
        path = System.IO.Path.Combine(Application.persistentDataPath,userInput);
        //check path exist
        try 
        {
            if(Directory.Exists(path))
            {
                //deletes all of the currently loaded roads and hubs and units 
                RoadManager roadManager = Camera.main.GetComponent<RoadManager>();
                foreach (Hub hub in roadManager.hubs.ToArray())
                {
                    //deletes all the cars w
                    foreach(Unit unit in hub.units.ToArray())
                    {
                        hub.units.Remove(unit);
                        UnityEngine.Object.Destroy(unit.gameObject);
                    }
                    roadManager.hubs.Remove(hub);
                    UnityEngine.Object.Destroy(hub.gameObject);
                }
                //deletes all the roads by removing all of the points 
                // then creates an itential list which loops through all path creators
                // remove the path creator from the list 
                roadManager.roads.Clear();
                roadManager.points.Clear();
                List<PathCreator> pathC = new List<PathCreator>();
                pathC = roadManager.pathCreators;
                foreach(PathCreator pathCreator in pathC.ToArray())
                {
                    Destroy(pathCreator.gameObject);
                    roadManager.pathCreators.Remove(pathCreator);
                }
                //starts coroutine to load hubs 
                LoadHubs(path);
                //starts coroutine to load roads
                StartCoroutine(LoadRoad());
            }
            else
            {
                inputField.text = "invalid input";
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e);
            inputField.text = "error";
            
        }
        recentSavePath = path;
        IsSavingOrLoading = false;
    }
    //Coroutine to load Roads
    private IEnumerator LoadRoad()
    {
        //gets the roadmanager which will eventully be used 
        RoadManager roadManager = Camera.main.GetComponent<RoadManager>();
        int x = 0;
        while(true)
        {
            try
            {
                //load the road
                RoadDataSaveAble notchangedData = SaveAndLoad.LoadRoad(x,path);
                //conveert from RoadDataSaveAble to RoadData
                RoadData loadedData = notchangedData.RoadDataSaveAbleToRoadData(notchangedData);
                //create a new road in the road manager 
                roadManager.CreateNewRoad(loadedData);
            }
            catch (System.Exception)
            {
                break;
            }
            
            yield return new WaitForSeconds(0.1f);
            x += 1;
        }
        //makes the ui unable to be seen
        parentGameObject.SetActive(false);
        yield return null;
    }
    //Coroutine to load hubs 
    private void LoadHubs(string path)
    {
        //gets the roadmanager which will eventully be used 
        RoadManager roadManager = Camera.main.GetComponent<RoadManager>();
        int x = 0;
        //while loop only left once loaded in all hubs 
        while(true)
        {
            try
            {   
                //loads the hub data 
                HubData hubData = SaveAndLoad.LoadHubs(x,path);
                //creates a new hub in the road manager 
                roadManager.CreateNewHub(hubData);
            }
            catch
            {
                break;
            }
            x += 1;
        }
    }
}
