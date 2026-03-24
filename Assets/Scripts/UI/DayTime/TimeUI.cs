using UnityEngine;
using TMPro;
using System;

public class TimeUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI dayText;

    private const int MAX_HOURS = 24;
    private const int MIDDAY_HOURS = 12;
    private const int MAX_MINUTES = 60;

    private const string AM_SUFFIX = "AM";
    private const string PM_SUFFIX = "PM";
    private const string M_SUFFIX = "M";

    private const string DOUBLE_DOT_CHARACTER = ":";

    private void OnEnable()
    {
        DayTimeManager.OnTimeInitialized += DayTimeManager_OnTimeInitialized;
        DayTimeManager.OnTimeChanged += DayTimeManager_OnTimeChanged;
    }

    private void OnDisable()
    {
        DayTimeManager.OnTimeInitialized -= DayTimeManager_OnTimeInitialized;
        DayTimeManager.OnTimeChanged -= DayTimeManager_OnTimeChanged;
    }


    private void SetTimeText(int time)
    {
        int fixedTime = time % (MAX_HOURS * MAX_MINUTES);

        int hours = fixedTime / MAX_MINUTES;
        int minutes = fixedTime % MAX_MINUTES;

        string period = "";

        if (hours == 12 && minutes == 0)
        {
            period = M_SUFFIX;
        }
        else
        {
            period = hours >= MIDDAY_HOURS ? PM_SUFFIX : AM_SUFFIX;
        }

        int displayHour = hours % MIDDAY_HOURS;
        if (displayHour == 0) displayHour = 12;

        dayText.text = $"{displayHour:00}:{minutes:00} {period}";
    }

    private void DayTimeManager_OnTimeInitialized(object sender, DayTimeManager.OnTimeEventArgs e)
    {
        SetTimeText(e.time);
    }

    private void DayTimeManager_OnTimeChanged(object sender, DayTimeManager.OnTimeEventArgs e)
    {
        SetTimeText(e.time);
    }
}
