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

    [Header("Energy")]
    [Range(5, 100)] public int maxEnergy;

    [Header("Inventory Starting Conditions")]
    public List<InventoryObjectIDQuantity> inventoryStartingConditions;

    [Header("Totora Crops")]
    public List<TotoraCropData> startingTotoraCrops;
}
