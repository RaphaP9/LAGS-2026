using UnityEngine;

[System.Serializable]
public class ActivitySchedule
{
    public ActivitySO activitySO;
    [Range(1, 10)] public int times;
    [TextArea(3, 10)] public string description;
}
