using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerData", order = 1)]
public class PlayerData : ScriptableObject, ISaveable
{
    [field: SerializeField] public float FuelAmount { get; private set; } = 100f;
    [field: SerializeField] public int Lives { get; private set; } = 3;
    [field: SerializeField] public int Health { get; private set; } = 5;

    

    public void DrainFuel(float fuelDrainRate, float deltaTime) 
    {
        FuelAmount -= fuelDrainRate * deltaTime;
        UIManager.instance.UpdateFuelDisplay();
    }

    public void SubtractFuel(float amountToSubtract) 
    {
        FuelAmount -= amountToSubtract;
        UIManager.instance.UpdateFuelDisplay();
    }

    public void AddFuel(float amountToAdd) 
    {
        FuelAmount += amountToAdd;
        if(FuelAmount >= 100) 
        {
            SetFuel(100);
        }
        UIManager.instance.UpdateFuelDisplay();
    }

    public void SetFuel(float newAmount) 
    {
        FuelAmount = newAmount;
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

    public object CaptureState()
    {
        return this;
    }

    public void RestoreState(object state)
    {
        if (FuelAmount == 0 || Health == 0) 
        {
            FuelAmount = 100f;
            Health = 5;
            
        }
        if(Lives == 0) 
        {
            //restart game
            Lives = 3;
        }

        FuelAmount = (float)state;
        Health = (int)state;
        Lives = (int)state;
    }

    public void SetDefaultStats() 
    {
        FuelAmount = 100f;
        Health = 5;
        Lives = 3;
    }
}
