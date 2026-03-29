using System;
using UnityEngine;

public class DayTimeManager : MonoBehaviour
{
    public static DayTimeManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Settings")]
    [SerializeField,Range(0f,60f)] private float minutesInGamePerSecondRealtime;

    [Header("Runtime Filled")]
    [SerializeField] private int currentDay;
    [SerializeField] private int currentTime;
    [SerializeField] private float currentRawTime;
    [Space]
    [SerializeField] private bool dayEnded;

    public int CurrentDay => currentDay;
    public float CurrentTime => currentTime;

    public static event EventHandler<OnDayEventArgs> OnDayInitialized;
    public static event EventHandler<OnTimeEventArgs> OnTimeInitialized;

    public static event EventHandler<OnDayEventArgs> OnDayEnd;
    public static event EventHandler<OnTimeEventArgs> OnTimeChanged;

    private const int MAX_HOURS = 24; 
    private const int MAX_MINUTES = 60;

    public class OnDayEventArgs : EventArgs
    {
        public int day;
    }

    public class OnTimeEventArgs : EventArgs
    {
        public int time;
    }

    private void OnEnable()
    {
        ActivitiesManager.OnActivityPerformed += ActivitiesManager_OnActivityPerformed;
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
            //Debug.LogWarning("There is more than one FishingManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeDay();
        InitializeTime();
    }

    private void Update()
    {
        HandleTimePassing();
    }

    private void InitializeDay()
    {
        currentDay = StaticDataManager.Instance.Data.currentDay;
        OnDayInitialized?.Invoke(this, new OnDayEventArgs { day = currentDay });
    }

    private void InitializeTime()
    {
        currentTime = StaticDataManager.Instance.Data.currentTime;
        OnTimeInitialized?.Invoke(this, new OnTimeEventArgs { time = currentTime });

        currentRawTime = currentTime;
    }

    private void HandleTimePassing()
    {
        if (!CanPassTime()) return;

        currentRawTime += Time.deltaTime * minutesInGamePerSecondRealtime;

        int newTime = ProcessCurrentRawTime();

        if (newTime != currentTime)
        {
            currentTime = newTime;
            OnTimeChanged?.Invoke(this, new OnTimeEventArgs {time = currentTime});

            StaticDataManager.Instance.SetCurrentTime(currentTime);
        }

        if(currentTime >= gameSettingsSO.finalTime)
        {
            dayEnded = true;
            OnDayEnd?.Invoke(this, new OnDayEventArgs { day = currentDay });

            PrepareForNextDay();
        }
        else
        {
            dayEnded = false;
        }
    }

    private int ProcessCurrentRawTime()
    {
        int processedTime = Mathf.FloorToInt(currentRawTime);
        processedTime = processedTime % (MAX_HOURS * MAX_MINUTES);

        return processedTime;
    }

    private bool CanPassTime()
    {
        if(dayEnded) return false;

        if (ScenesManager.Instance.SceneState != ScenesManager.State.Idle) return false;

        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.GamePaused) return false;
        }

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.GameState != GameManager.State.Exploring) return false;
        }

        return true;
    }

    public bool IsCurrentTimeBetweenTwoTimes(int timeA, int timeB)
    {
        return GeneralUtilities.IsBetween(timeA, timeB, currentTime);
    }

    public void AddTime(int timeToAdd)
    {
        currentRawTime += timeToAdd;

        currentTime = ProcessCurrentRawTime();
        OnTimeChanged?.Invoke(this, new OnTimeEventArgs { time = currentTime });

        StaticDataManager.Instance.SetCurrentTime(currentTime);
    }

    public void AddTimeWithoutNotify(int timeToAdd)
    {
        currentRawTime += timeToAdd;

        currentTime = ProcessCurrentRawTime();

        StaticDataManager.Instance.SetCurrentTime(currentTime);
    }

    public void SetTime(int time)
    {
        currentRawTime = time;
        currentTime = ProcessCurrentRawTime();
        OnTimeChanged?.Invoke(this, new OnTimeEventArgs { time = currentTime });

        StaticDataManager.Instance.SetCurrentTime(currentTime);
    }

    public void SetTimeWithoutNotify(int time)
    {
        currentRawTime = time;
        currentTime = ProcessCurrentRawTime();

        StaticDataManager.Instance.SetCurrentTime(currentTime);
    }

    public void ResetTimeToStart() => SetTimeWithoutNotify(gameSettingsSO.startingTime);

    public void TurnToNextDay() //NeverTrigger Events For This
    {
        if (IsLastDay()) return;

        currentDay++;
        StaticDataManager.Instance.SetCurrentDay(currentDay);
    }

    public bool IsLastDay()
    {
        return currentDay >= gameSettingsSO.finalDay;
    }

    public void PrepareForNextDay()
    {
        StaticDataManager.Instance.ResetActivitiesPerformed();
        StaticDataManager.Instance.ResetActivitiesPerformedSuccessfully();
        StaticDataManager.Instance.ResetCurrentPlayerPosition();
        StaticDataManager.Instance.SetHasStartedDay(false);

        ResetTimeToStart();
        TurnToNextDay();
    }

    public float GetNormalizedTime()
    {
        float normalizedTime = Mathf.InverseLerp(gameSettingsSO.startingTime, gameSettingsSO.finalTime, currentTime);
        return normalizedTime;
    }

    #region Subscriptions
    private void ActivitiesManager_OnActivityPerformed(object sender, ActivitiesManager.OnActivityPerformedEventArgs e)
    {
        AddTime(e.activitySO.timeAdd);
    }
    #endregion
}