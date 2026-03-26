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
    public int startingTime;
    public int finalTime;

    [Header("Activities")]
    [Range(0, 600)] public int timeAddPerFishing;
    [Range(0, 600)] public int timeAddPerWeave;
    [Range(0, 600)] public int timeAddPerHarvest;

    [Header("Inventory Starting Conditions")]
    public List<InventoryObjectIDQuantity> inventoryStartingConditions;

    [Header("Totora Crops")]
    public List<TotoraCropData> startingTotoraCrops;
}
