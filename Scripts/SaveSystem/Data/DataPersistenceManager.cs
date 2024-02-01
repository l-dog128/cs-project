using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private BackGroundData backGroundData;
    private List<IDataPersitence> DataPersitanceObject;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance{get; private set;}

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("more than one instance");
        }
        instance = this;
    }
    private void Start()
    {
        //data is stored in C:\Users\Lewis Conroy\AppData\LocalLow\DefaultCompany\cs project Draft 3
        this.dataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
        this.DataPersitanceObject = FindAllDataPersitanceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.backGroundData = new BackGroundData();
    }
    public void LoadGame()
    {
        // load saved data  from a file 
        this.backGroundData = dataHandler.Load();

        //if no data new game
        if(this.backGroundData == null)
        {
            Debug.Log("no data was found intializing to default.");
            NewGame();
        }
        // push loaded data to all other scripts 
        foreach(IDataPersitence dataPersitence in  DataPersitanceObject)
        {
            dataPersitence.LoadData(backGroundData);
        }
    }   
    public void SaveGame()
    {
        //pass data to other scripts
        foreach(IDataPersitence dataPersitence in  DataPersitanceObject)
        {
            dataPersitence.SaveData( ref backGroundData);
        }

        //save data to a file 
        dataHandler.Save(backGroundData);

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersitence> FindAllDataPersitanceObjects()
    {
        IEnumerable<IDataPersitence> dataPersitenceObejects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersitence>();

        return new List<IDataPersitence>(dataPersitenceObejects);
    }
}
