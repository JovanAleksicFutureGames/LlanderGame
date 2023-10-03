using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    /// <summary>
    /// Base file name for save flle
    /// Written by RobAnthem
    /// Code found at: https://forum.unity.com/threads/complex-save-system-example.897968/
    /// </summary>
    public string saveFile = "savedData"; // needs an identifier if you want multiple save slots.
    /// <summary>
    /// The objects that wll be saved.
    /// </summary>
    public List<GameObject> saveObjects;
    /// <summary>
    /// Function to locate any objects that can be saved.
    /// </summary>

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void FindSaveObjects()
    {
        // We use a gameobject list because you can't find Interfaces and the system will lose references to them.
        saveObjects = new List<GameObject>();
        // FInd al l objects, even in a large scene this may only take a second or 2.
        GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
        // Iterate the objects.
        foreach (GameObject go in gos)
        {
            // Look for savables
            if (go.GetComponent<ISaveable>() != null)
            {
                // If found, add it to our list
                saveObjects.Add(go);
            }
        }
    }
    /// <summary>
    /// Save all current saveable scene data.
    /// </summary>
    public void SaveData()
    {
        //Check if initialized
        if (saveObjects == null || saveObjects.Count == 0)
            FindSaveObjects();
        // Create our data object
        Dictionary<string, Dictionary<string, object>> allData = new Dictionary<string, Dictionary<string, object>>();
        // Collect all the data.
        foreach (GameObject go in saveObjects)
        {
            ISaveable isave = go.GetComponent<ISaveable>();
            allData.Add(isave.UniqueID, isave.OnSave());
        }
        //Save the data.
        SaveManager.Instance.SaveData(allData, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + saveFile);
    }
    /// <summary>
    /// Load the data stucture from the file system.
    /// </summary>
    public void LoadData()
    {
        //Check if we have initialized
        if (saveObjects == null || saveObjects.Count == 0)
            FindSaveObjects();
        //Get our data
        Dictionary<string, Dictionary<string, object>> allData = SaveManager.Instance.LoadData<Dictionary<string, Dictionary<string, object>>>(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + saveFile);
        if (allData == null)
        {
            Debug.LogWarning("Save File NOT FOUND");
            return;
        }
        //Iterate and load onto our objects
        foreach (GameObject go in saveObjects)
        {
            ISaveable isave = go.GetComponent<ISaveable>();
            isave.OnLoad(allData[isave.UniqueID]);
        }
    }
}
