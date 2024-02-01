using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class SaveAndLoad
{
    // Method to save the road data 
    // takes RoadData to be converted 
    // takes int for file number 
    // path where file is saved
    public static void SaveData(RoadData roadData,int num,string path)
    {
        // Create new binary formatter and new path
        BinaryFormatter formatter = new BinaryFormatter();
        path = path + "/roads.road" + num.ToString();
        // Create new stream
        FileStream stream = new FileStream(path,FileMode.Create);

        // Convert roadData to Road data SaveAble
        RoadDataSaveAble roadDataSaveAble = new RoadDataSaveAble(roadData);
        //Write the data
        formatter.Serialize(stream,roadDataSaveAble);
        stream.Close();
    }

    // Method that loads the data and returns as RoadDataSaveAble
    // takes num and path for to find file     
    public static RoadDataSaveAble LoadRoad(int num,string path)
    {
        // Create new path and check it exists 
        path = path + "/roads.road"+ num.ToString();
        if(File.Exists(path))
        {
            // Create new fromatter and stream
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            // Deserilazie data and return it 
            RoadDataSaveAble data = formatter.Deserialize(stream) as RoadDataSaveAble;
            stream.Close();
            return data;
        }
        else
        {
            //Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }

    // Method to save the Hubs , same as road excpet converts hub to hubData
    // takes hub to be saved 
    // takes int for file number 
    // path where file is saved
    public static void SaveHubs(Hub hub,int num,string path)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        path = path + "/hubs.hub" + num.ToString()  ;

        FileStream stream = new FileStream(path, FileMode.Create);
        // Create new hubdata
        HubData hubData = new HubData(hub);

        formatter.Serialize(stream, hubData);
        stream.Close();
    }
    // Method that loads the data and returns as HubData
    // takes num and path for to find file   
    public static HubData LoadHubs(int num,string path)
    {
        path = path + "/hubs.hub" + num.ToString();

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
    // Method that saves the simulation results 
    // Takes path and object array
    public static void SaveSimulationResults(string path,object[] results)
    {
        // Combine and create a new folder
        path = System.IO.Path.Combine(path ,"Results");
        DirectoryInfo folder = Directory.CreateDirectory(path);
        
        // Create new .txt file 
        path = path + "/results.txt";
        StreamWriter writer = new StreamWriter(path,true);
        writer.WriteLine("---------------");
        writer.WriteLine("Complexity" +": " + results[0].ToString());
        writer.WriteLine("Material Used" +": " + results[1].ToString() + " Tons");
        writer.WriteLine("Speed" +": " + results[2].ToString());
        writer.WriteLine("Score" +": " + results[3].ToString());
        writer.Close();

    }
}
