using UnityEngine;

[System.Serializable]
public class Data
{
    public int currentEnergy
        ;
    public int timesPlayedFishingGame;

    public Data(GameSettingsSO gameSettingsSO)
    {
        ResetData(gameSettingsSO);
    }

    public void ResetData(GameSettingsSO gameSettingsSO)
    {
        currentEnergy = gameSettingsSO.maxEnergy;
        timesPlayedFishingGame = 0;
    }
}
