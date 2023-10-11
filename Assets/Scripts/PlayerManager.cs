using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [field:SerializeField] private List<PlayerController> playerList = new List<PlayerController>();
    public static PlayerManager instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            CleanUpObject();

        foreach (PlayerController player in GameObject.FindObjectsOfType<PlayerController>())
        {
            playerList.Add(player);
        }
    }

    private void CleanUpObject() 
    {
        Destroy(gameObject);
    }

    public PlayerController GetPlayer(int i)
    {
        return playerList[i];
    }
}
