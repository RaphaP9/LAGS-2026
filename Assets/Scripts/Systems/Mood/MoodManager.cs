using System;
using System.Collections.Generic;
using UnityEngine;

public class MoodManager : MonoBehaviour
{
    public static MoodManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Runtime Filled")]
    [SerializeField] private int currentMood;

    public static event EventHandler<OnMoodEventArgs> OnMoodInitialized;
    public static event EventHandler<OnMoodEventArgs> OnMoodChanged;

    public class OnMoodEventArgs : EventArgs
    {
        public int mood;
    }

    private void OnEnable()
    {
        ActivitiesManager.OnActivityPerformed += ActivitiesManager_OnActivityPerformed;
        ActivitiesManager.OnActivityPerformedSuccess += ActivitiesManager_OnActivityPerformedSuccess;
    }

    private void OnDisable()
    {
        ActivitiesManager.OnActivityPerformed -= ActivitiesManager_OnActivityPerformed;
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
            //Debug.LogWarning("There is more than one MoodManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeMood();
    }

    private void InitializeMood()
    {
        currentMood = StaticDataManager.Instance.Data.currentMood;
        OnMoodInitialized?.Invoke(this, new OnMoodEventArgs { mood = currentMood });
    }

    private void EvaluateProductiveActivitiesInARow(List<ActivitySO> activitiesPerformed)
    {

    }

    private void ChangeMood(int moodChange)
    {
        if(moodChange>0) currentMood = currentMood + moodChange > gameSettingsSO.maxMood? gameSettingsSO.maxMood : currentMood + moodChange;
        else currentMood = currentMood + moodChange < gameSettingsSO.minMood? gameSettingsSO.minMood : currentMood + moodChange;

        StaticDataManager.Instance.SetCurrentMood(currentMood);

        OnMoodChanged?.Invoke(this, new OnMoodEventArgs { mood = currentMood });
    }

    #region Subscriptions
    private void ActivitiesManager_OnActivityPerformed(object sender, ActivitiesManager.OnActivityPerformedEventArgs e)
    {
        EvaluateProductiveActivitiesInARow(e.activitiesPerformed);
    }

    private void ActivitiesManager_OnActivityPerformedSuccess(object sender, ActivitiesManager.OnActivityPerformedEventArgs e)
    {
        ChangeMood(e.activitySO.moodChange);
    }
    #endregion
}
