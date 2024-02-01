using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public BackGroundData Load()
    {
        string fullPath = System.IO.Path.Combine(dataDirPath,dataFileName);
        BackGroundData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //load serilized data
                string dataToLoad = "";
                using(FileStream stream = new FileStream (fullPath,FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //dersilazie data from json to c#
                loadedData = JsonUtility.FromJson<BackGroundData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("error when trying to load" + e );
            }
        }
        return loadedData;

    }

    public void Save(BackGroundData data)
    {
        string fullPath = System.IO.Path.Combine(dataDirPath,dataFileName);
        try
        {
            // create dir path 
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fullPath));

            //serialize c# data to json
            string dataToStore = JsonUtility.ToJson(data,true);

            //write file to system
            using(FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            
            Debug.LogError("Error when trying to save file: " + fullPath + "\n" + e);
        }
    }
}
