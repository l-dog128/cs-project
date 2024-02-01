using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveAndLoadPart2
{
    public static void SaveData(RoadData roadData,int num)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/roads.road" + num.ToString();

        FileStream stream = new FileStream(path,FileMode.Create);

        RoadDataData roadDataData = new RoadDataData(roadData);
        formatter.Serialize(stream,roadDataData);
        stream.Close();
    }



    //going to have to do multiple times 
     
    public static RoadDataData LoadData(int num)
    {
        string path = Application.persistentDataPath + "/roads.road"+ num.ToString();
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RoadDataData data = formatter.Deserialize(stream) as RoadDataData;
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }


    public static void SaveHubs(Hub hub,int num)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/hubs.hub" + num.ToString()  ;

        FileStream stream = new FileStream(path, FileMode.Create);

        HubData hubData = new HubData(hub);

        formatter.Serialize(stream, hubData);
        stream.Close();
    }
    public static HubData LoadHubs(int num)
    {
        string path = Application.persistentDataPath + "/hubs.hub" + num.ToString();

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HubData data = formatter.Deserialize(stream) as HubData;

            stream.Close();

            return data;
        } else
        {
            //Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }
}
