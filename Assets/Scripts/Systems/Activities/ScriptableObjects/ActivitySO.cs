using UnityEngine;

[CreateAssetMenu(fileName = "NewActivitySO", menuName = "ScriptableObjects/Activity")]
public class ActivitySO : ScriptableObject
{
    public string activityName;
    public string description;
    [Space]
    [Range(-10, 10)] public int moodChange;
    public bool isProductive;
    [Space]
    [Range(0, 600)] public int timeAdd;
}
