using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class PlayerData : MonoBehaviour, ISaveable
{
    public static PlayerData instance;

    [field: SerializeField] public float _fuelAmount { get; private set; }
    [field: SerializeField] public int Lives { get; private set; }
    [field: SerializeField] public int Health { get; private set; }

    public string m_UniqueID = "PlayerData";
    public string UniqueID {get { return m_UniqueID; }set { m_UniqueID = value; } }

    private void Awake()
    {
        instance = this;
    }

    public void DrainFuel(float fuelDrainRate, float deltaTime) 
    {
        _fuelAmount -= fuelDrainRate * deltaTime;
        UIManager.instance.UpdateFuelDisplay();
    }

    public void SubtractFuel(float amountToSubtract) 
    {
        _fuelAmount -= amountToSubtract;
        UIManager.instance.UpdateFuelDisplay();
    }

    public void AddFuel(float amountToAdd) 
    {
        _fuelAmount += amountToAdd;
        if(_fuelAmount >= 100) 
        {
            SetFuel(100);
        }
        UIManager.instance.UpdateFuelDisplay();
    }

    public void SetFuel(float newAmount) 
    {
        _fuelAmount = newAmount;
        UIManager.instance.UpdateFuelDisplay();
    }
    public void AddHealth(int amountToadd) 
    {
        Health += amountToadd;
        if(Health >= 5) 
        {
            Health = 5;
        }
        UIManager.instance.UpdateHealthDisplay();
    }

    public void DecrementHealth() 
    {
        Health--;
        UIManager.instance.UpdateHealthDisplay();
    }

    public void SetHealth(int amount) 
    {
        Health = amount;
        UIManager.instance.UpdateHealthDisplay();
    }

    public void DecrementLives() 
    {
        Lives--;
        if (Lives <= 0) Lives = 0;
        UIManager.instance.DisplayLives();
    }

    public void SetLives(int amount) 
    {
        Lives = amount;
        UIManager.instance.DisplayLives();
    }

    public Dictionary<string, object> OnSave()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add(nameof(_fuelAmount), _fuelAmount);
        data.Add(nameof(Lives), Lives);
        return data;
    }

    public void OnLoad(Dictionary<string, object> data)
    {
        _fuelAmount = (float)data[nameof(_fuelAmount)];
        Lives = (int)data[nameof(Lives)];
    }
}
