using System.Data;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem
{
    private string dataPath = Application.persistentDataPath + "/saveData.sav";


    public void SaveData()
    {
        if (File.Exists(dataPath))
        {
            FileStream dataStream = new FileStream(dataPath, FileMode.Create);
            //write data
            dataStream.Close();
        }
    }

    public void LoadData() 
    {
        if (!File.Exists(dataPath)) 
        {
            FileStream outputStream = new FileStream(dataPath, FileMode.Create);
            //read data
            outputStream.Close();
        }
        FileStream inputStream = new FileStream(dataPath, FileMode.Open);
        //read data
        //apply data
        inputStream.Close();
    }
}
