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
          + $"/Maps/{name}.map");
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    public void LoadGame<T>(ref T data, string name)
    {
        if (File.Exists(Application.persistentDataPath
          + $"/Maps/{name}.map"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + $"/Maps/{name}.map", FileMode.Open);
            data = (T)bf.Deserialize(file);
            file.Close();

            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }

    public List<string> GetNameMaps()
    {
        List<string> maps = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/Maps/");
        Directory.CreateDirectory(Application.persistentDataPath + "/Maps/");
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            if (f.Extension == ".map")
            {
                var t = f.Name;
                maps.Add(t.Remove(t.Length - 4));
            }
        }
        return maps;
    }
    public void DeleteData<T>(string name)
    {
        if (File.Exists(Application.persistentDataPath
          + $"/Maps/{name}.map"))
        {
            File.Delete(Application.persistentDataPath
              + $"/Maps/{name}.map");

            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}
