using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Data
{
    public int currentDay;
    public int currentTime;
    public int currentEnergy;
    [Space]
    public Vector2 currentPlayerPosition;
    [Space]
    public List<InventoryObjectIDQuantity> currentInventory;
    [Space]
    public int timesCooked;
    public int timesFished;
    public int timesHarvested;
    public int timesWoven;

    public Data(GameSettingsSO gameSettingsSO)
    {
        ResetData(gameSettingsSO);
    }

    public void ResetData(GameSettingsSO gameSettingsSO)
    {
        currentDay = gameSettingsSO.startingDay;
        currentTime = gameSettingsSO.startingTime;
        currentEnergy = gameSettingsSO.maxEnergy;

        currentPlayerPosition = gameSettingsSO.startingPlayerPosition;

        ResetInventory(gameSettingsSO);

        timesCooked = 0;
        timesFished = 0;
        timesHarvested = 0;
        timesWoven = 0;
    }

    public void ResetInventory(GameSettingsSO gameSettingsSO)
    {
        currentInventory = new List<InventoryObjectIDQuantity>();

        foreach(InventoryObjectIDQuantity inventoryObjectIDQuantity in gameSettingsSO.inventoryStartingConditions)
        {
            InventoryObjectIDQuantity newInventoryObjectIDQuantity = new InventoryObjectIDQuantity { inventoryObjectID = inventoryObjectIDQuantity.inventoryObjectID, quantity = inventoryObjectIDQuantity.quantity };
            currentInventory.Add(newInventoryObjectIDQuantity);
        }
    }
}
