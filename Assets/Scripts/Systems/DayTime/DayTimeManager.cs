using System;
using UnityEngine;

public class DayTimeManager : MonoBehaviour
{
    public static DayTimeManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private GameSettingsSO gameSettingsSO;

    [Header("Settings")]
    [SerializeField,Range(0f,30f)] private float minutesInGamePerSecondRealtime;

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

        int newTime = Mathf.FloorToInt(currentRawTime);
        newTime = newTime % (MAX_HOURS * MAX_MINUTES);

        if(newTime != currentTime)
        {
            currentTime = newTime;
            OnTimeChanged?.Invoke(this, new OnTimeEventArgs {time = currentTime});

            StaticDataManager.Instance.SetCurrentTime(currentTime);
        }

        if(currentTime >= gameSettingsSO.finalTime)
        {
            dayEnded = true;
            OnDayEnd?.Invoke(this, new OnDayEventArgs { day = currentDay });
        }
        else
        {
            dayEnded = false;
        }
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
}
