using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveMethod2 : MonoBehaviour
{
    public List<RoadData> roads;
    public List<Hub> hubs;

    RoadManager roadManager;

    public bool save = false;
    public bool load = false;


    void Awake()
    {
        roadManager = GetComponent<RoadManager>();
    }

    void Update()
    {
        if (save == true)
        {
            SaveGame();
            save = false;
        }
        if(load == true)
        {
            LoadGame();
            load = false;
        }
    }
    void SaveGame()
    {
        roads = roadManager.roads;
        hubs = roadManager.hubs;
        
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat"); 
        SaveData data = new SaveData();
        data.roads = roads;
        data.hubs = hubs;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath+"/MySaveData.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath+"/MySaveData.dat",FileMode.Open);
            SaveData data = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();
            roads = data.roads;
            hubs = data.hubs;
            roadManager.roads = roads;
            roadManager.hubs = hubs;

            Debug.Log("Data Loaded");
        }
        else
        {
            Debug.LogError("no sava data");
        }
    }

}

[Serializable]
class SaveData
{
    public List<RoadData> roads;
    public List<Hub> hubs;
}