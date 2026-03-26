using UnityEngine;

[CreateAssetMenu(fileName = "NewActivitySO", menuName = "ScriptableObjects/Activity")]
public class ActivitySO : ScriptableObject
{
    public string activityName;
    public string description;
    [Space]
    [Range(-100, 100)] public int moodChange;
    public bool isProductive;
    [Space]
    [Range(0, 600)] public int timeAdd;
}
