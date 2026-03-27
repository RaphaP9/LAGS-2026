using UnityEngine;

[System.Serializable]
public class ActivitySchedulePerformed
{
    public ActivitySO activitySO;
    public int timesPerformed;
    public int times;
    [TextArea(3, 10)] public string description;
}
