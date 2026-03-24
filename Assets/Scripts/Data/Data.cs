using UnityEngine;

[System.Serializable]
public class Data
{
    public Vector2 currentPlayerPosition;

    public int currentDay;
    public int currentTime;

    public int currentEnergy;
    public int timesPlayedFishingGame;

    public Data(GameSettingsSO gameSettingsSO)
    {
        ResetData(gameSettingsSO);
    }

    public void ResetData(GameSettingsSO gameSettingsSO)
    {
        currentPlayerPosition = gameSettingsSO.startingPlayerPosition;
        currentEnergy = gameSettingsSO.maxEnergy;
        timesPlayedFishingGame = 0;
    }
}
