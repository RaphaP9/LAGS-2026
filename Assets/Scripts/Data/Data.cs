using UnityEngine;

[System.Serializable]
public class Data
{
    public int timesPlayedFishingGame;

    public Data()
    {
        ResetData();
    }

    public void ResetData()
    {
        timesPlayedFishingGame = 0;
    }
}
