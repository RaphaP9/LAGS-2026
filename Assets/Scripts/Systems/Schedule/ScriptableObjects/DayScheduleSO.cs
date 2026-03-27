using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewMusicPoolSO", menuName = "ScriptableObjects/Activities/DaySchedule")]
public class DayScheduleSO : ScriptableObject
{
    public List<ActivitySchedule> activityScheduleList;
}
