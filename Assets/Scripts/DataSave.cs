using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSave
{
    public void SaveGame<T>(T data, string name)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + $"/{name}.map");
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    public void LoadGame<T>(ref T data, string name)
    {
        if (File.Exists(Application.persistentDataPath
          + $"/{name}.map"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + $"/{name}.map", FileMode.Open);
            data = (T)bf.Deserialize(file);
            file.Close();

            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    public void DeleteData<T>(string name)
    {
        if (File.Exists(Application.persistentDataPath
          + $"/{name}.map"))
        {
            File.Delete(Application.persistentDataPath
              + $"/{name}.map");

            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}
