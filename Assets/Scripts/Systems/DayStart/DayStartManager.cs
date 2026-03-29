using System;
using UnityEngine;

public class DayStartManager : MonoBehaviour
{
    public static event EventHandler<OnDayStartEventArgs> OnDayStart;

    public class OnDayStartEventArgs : EventArgs
    {
        public int day;
    }

    private void OnEnable()
    {
        DayTimeManager.OnDayInitialized += DayTimeManager_OnDayInitialized;
        IntroductionManager.OnIntroductionEnd += IntroductionManager_OnIntroductionEnd;
    }

    private void OnDisable()
    {
        DayTimeManager.OnDayInitialized -= DayTimeManager_OnDayInitialized;
        IntroductionManager.OnIntroductionEnd -= IntroductionManager_OnIntroductionEnd;
    }

    private void HandleDayStart()
    {
        if (StaticDataManager.Instance.Data.hasStartedDay) return;
        if (!StaticDataManager.Instance.Data.hasIntroducted) return;

        StaticDataManager.Instance.SetHasStartedDay(true);

        OnDayStart?.Invoke(this, new OnDayStartEventArgs { day = DayTimeManager.Instance.CurrentDay });
    }

    private void DayTimeManager_OnDayInitialized(object sender, DayTimeManager.OnDayEventArgs e)
    {
        HandleDayStart();
    }

    private void IntroductionManager_OnIntroductionEnd(object sender, EventArgs e)
    {
        HandleDayStart();
    }
}
