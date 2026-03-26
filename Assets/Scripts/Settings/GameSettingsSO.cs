using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "NewGameSettingsSO", menuName = "ScriptableObjects/Game/Settings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Player Position")]
    public Vector2 startingPlayerPosition;

    [Header("Days")]
    public int startingDay;
    public int finalDay;

    [Header("Time - In Minutes")]
    [Range(0,1440)] public int startingTime;
    [Range(0, 1440)] public int finalTime;

    [Header("Mood")]
    [Range(0, 100)] public int startingMood;
    [Range(0, 100)] public int maxMood;
    [Range(0, 100)] public int minMood;
    [Space]
    public List<MoodPenalizations> moodPenalizations;


    [Header("Activities")]
    [Range(0, 600)] public int timeAddPerFishing;
    [Range(0, 600)] public int timeAddPerWeave;
    [Range(0, 600)] public int timeAddPerHarvest;

    [Header("Inventory Starting Conditions")]
    public List<InventoryObjectIDQuantity> inventoryStartingConditions;

    [Header("Totora Crops")]
    public List<TotoraCropData> startingTotoraCrops;
}

[System.Serializable]
public class MoodPenalizations
{
    [Range(2,10)] public int productiveActivitiesInARow;
    [Range(-10, 0)] public int moodChange;
}
