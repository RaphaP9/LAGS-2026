using UnityEngine;
using System.Collections.Generic;

public class StaticDataManager : MonoBehaviour
{
    public static StaticDataManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Data")]
    [SerializeField] private Data data;

    public Data Data => data;

    private void Awake()
    {
        SetSingleton();
        ResetData(gameSettingsSO);
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.LogWarning("There is more than one StaticDataManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    public void ResetData(GameSettingsSO gameSettingsSO) => data.ResetData(gameSettingsSO);

    ///////////////////////////////////////////////////////////////////////////////////////////////
    

    public void SetCurrentPlayerPosition(Vector2 playerPosition) => data.currentPlayerPosition = playerPosition;

    public void SetCurrentTime(int time) => data.currentTime = time;
    public void SetCurrentDay(int day) => data.currentDay = day;

    ///////////////////////////////////////////////////////////////////////////////////////////////

    public void IncreaseTimesCooked(int quantity) => data.timesCooked += quantity;
    public void IncreaseTimesFished(int quantity) => data.timesFished += quantity;
    public void IncreaseTimesHarvested(int quantity) => data.timesHarvested += quantity;
    public void IncreaseTimesWoven(int quantity) => data.timesWoven += quantity;

    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetCurrentInventory(List<InventoryObjectQuantity> inventory)
    {
        foreach (InventoryObjectQuantity item in inventory)
        {
            foreach(InventoryObjectIDQuantity inventoryObjectIDQuantity in data.currentInventory)
            {
                if(item.inventoryObjectSO.id == inventoryObjectIDQuantity.inventoryObjectID)
                {
                    inventoryObjectIDQuantity.quantity = item.quantity;
                    continue;
                }
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////

    public void SetTotoraCropHarvested(int totoraCropID)
    {
        foreach(TotoraCropData totoraCropData in data.currentTotoraCrops)
        {
            if(totoraCropData.id == totoraCropID)
            {
                totoraCropData.isHarvested = true;
                return;
            }
        }
    }

    public bool GetTotoraCropHarvested(int totoraCropID)
    {
        foreach (TotoraCropData totoraCropData in data.currentTotoraCrops)
        {
            if (totoraCropData.id == totoraCropID) return totoraCropData.isHarvested;
        }

        return false;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void SetCurrentMood(int mood) => data.currentMood = mood;

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void ResetActivitiesPerformed() => data.ResetActivitiesPerformed();
}
