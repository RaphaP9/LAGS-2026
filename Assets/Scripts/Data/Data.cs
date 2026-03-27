using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Data
{
    public int currentDay;
    public int currentTime;
    [Space]
    public int currentMood;
    [Space]
    public Vector2 currentPlayerPosition;
    [Space]
    public List<InventoryObjectIDQuantity> currentInventory;
    [Space]
    public int timesCooked;
    public int timesFished;
    public int timesHarvested;
    public int timesWoven;
    [Space]
    public List<TotoraCropData> currentTotoraCrops;
    [Space]
    public List<ActivitySO> currentActivitiesPerformed;
    public List<ActivitySO> currentActivitiesPerformedSuccessfully;

    public Data(GameSettingsSO gameSettingsSO)
    {
        ResetData(gameSettingsSO);
    }

    public void ResetData(GameSettingsSO gameSettingsSO)
    {
        currentDay = gameSettingsSO.startingDay;
        currentTime = gameSettingsSO.startingTime;

        currentMood = gameSettingsSO.startingMood;

        currentPlayerPosition = gameSettingsSO.startingPlayerPosition;

        ResetInventory(gameSettingsSO);
        ResetTorotaCrops(gameSettingsSO);
        ResetActivitiesPerformed();
        ResetActivitiesPerformedSuccessfully();

        timesCooked = 0;
        timesFished = 0;
        timesHarvested = 0;
        timesWoven = 0;
    }

    public void ResetPlayerPosition(GameSettingsSO gameSettingsSO)
    {
        currentPlayerPosition = gameSettingsSO.startingPlayerPosition;
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

    public void ResetTorotaCrops(GameSettingsSO gameSettingsSO)
    {
        currentTotoraCrops = new List<TotoraCropData>();

        foreach (TotoraCropData totoraCropsData in gameSettingsSO.startingTotoraCrops)
        {
            TotoraCropData newTotoraCropsData = new TotoraCropData { id = totoraCropsData.id, isHarvested = totoraCropsData.isHarvested };
            currentTotoraCrops.Add(newTotoraCropsData);
        }
    }

    public void AddActivityPerformed(ActivitySO activitySO) => currentActivitiesPerformed.Add(activitySO);
    public void AddActivityPerformedSuccessfully(ActivitySO activitySO) => currentActivitiesPerformedSuccessfully.Add(activitySO);

    public void ResetActivitiesPerformed()
    {
        currentActivitiesPerformed = new List<ActivitySO>();
    }

    public void ResetActivitiesPerformedSuccessfully()
    {
        currentActivitiesPerformedSuccessfully = new List<ActivitySO>();
    }
}
