using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [field:SerializeField] private List<PlayerController> playerList = new List<PlayerController>();

    public static PlayerManager instance;



    private void Awake()
    {
        instance = this;

        foreach (PlayerController player in GameObject.FindObjectsOfType<PlayerController>())
        {
            playerList.Add(player);
        }
        AssignPlayers();
    }

    private void AssignPlayers() 
    {
        //playerList[0].SetIsPlayerOne(true);
    }

    public PlayerController GetPlayer(int i)
    {
        return playerList[i];
    }
}