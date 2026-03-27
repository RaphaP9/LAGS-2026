using UnityEngine;
using System.Collections.Generic;
using System;

public class ScheduleManager : MonoBehaviour
{
    public static ScheduleManager Instance { get; private set; }

    [Header("Lists")]
    [SerializeField] private List<DayScheduleRelation> dayScheduleRelations;

    [Header("RuntimeFilled")]
    [SerializeField] private List<ActivitySchedulePerformed> activitiesPerformedList;

    public static event EventHandler<OnActivityScheduleEventArgs> OnScheduleManagerInitialized;
    public static event EventHandler<OnActivityScheduleEventArgs> OnScheduleChange;

    [System.Serializable]
    public class DayScheduleRelation
    {
        public int day;
        public DayScheduleSO dayScheduleSO;
    }

    public class OnActivityScheduleEventArgs : EventArgs
    {
        public List<ActivitySchedulePerformed> activitiesPerformedList;
    }

    private void OnEnable()
    {
        ActivitiesManager.OnActivitiesPerformedInitialized += ActivitiesManager_OnActivitiesPerformedInitialized;
        ActivitiesManager.OnActivityPerformedSuccess += ActivitiesManager_OnActivityPerformedSuccess;
    }

    private void OnDisable()
    {
        ActivitiesManager.OnActivitiesPerformedInitialized -= ActivitiesManager_OnActivitiesPerformedInitialized;
        ActivitiesManager.OnActivityPerformedSuccess += ActivitiesManager_OnActivityPerformedSuccess;
    }

    private void Awake()
    {
        SetSingleton();
    }
    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one ScheduleManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private DayScheduleSO GetDayScheduleSOByDay(int day)
    {
        foreach (DayScheduleRelation relation in dayScheduleRelations)
        {
            if (relation.day == StaticDataManager.Instance.Data.currentDay)
            {
                return relation.dayScheduleSO;
            }
        }

        return null;
    }

    private int GetTimesPerformedActivitySuccessfully(ActivitySO activitySO)
    {
        int accumulator = 0;

        foreach(ActivitySO activity in ActivitiesManager.Instance.ActivitiesPerformedSuccessfully)
        {
            if(activity == activitySO) accumulator++;
        }

        return accumulator;
    }

    private void InitializeScheduleManager()
    {
        BuildActivitiesPerformedList();
        PopulateActivitiesPerformed();

        OnScheduleManagerInitialized?.Invoke(this, new OnActivityScheduleEventArgs { activitiesPerformedList = activitiesPerformedList });
    }

    private void UpdateSchedule()
    {
        PopulateActivitiesPerformed();
        OnScheduleChange?.Invoke(this, new OnActivityScheduleEventArgs { activitiesPerformedList = activitiesPerformedList });
    }

    private void BuildActivitiesPerformedList()
    {
        DayScheduleSO dayScheduleSO = GetDayScheduleSOByDay(DayTimeManager.Instance.CurrentDay);

        activitiesPerformedList.Clear();

        foreach(ActivitySchedule activitySchedule in dayScheduleSO.activityScheduleList)
        {
            ActivitySchedulePerformed activitySchedulePerformed = new ActivitySchedulePerformed { activitySO = activitySchedule.activitySO, timesPerformed = 0, times = activitySchedule.times, description = activitySchedule.description };
            activitiesPerformedList.Add(activitySchedulePerformed);
        }
    }

    private void PopulateActivitiesPerformed()
    {
        foreach(ActivitySchedulePerformed activitySchedulePerformed in activitiesPerformedList)
        {
            int timesPerformed = GetTimesPerformedActivitySuccessfully(activitySchedulePerformed.activitySO);
            activitySchedulePerformed.timesPerformed = timesPerformed;
        }
    }

    #region Subscriptions
    private void ActivitiesManager_OnActivitiesPerformedInitialized(object sender, ActivitiesManager.OnActivitiesEventArgs e)
    {
        InitializeScheduleManager();
    }

    private void ActivitiesManager_OnActivityPerformedSuccess(object sender, ActivitiesManager.OnActivityPerformedEventArgs e)
    {
        UpdateSchedule();
    }
    #endregion
}
