using UnityEngine;

public class ScheduleUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Animator animator;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private const string SHOWING_ANIMATION_NAME = "Showing";
    private const string HIDDEN_ANIMATION_NAME = "Hidden";

    private void OnEnable()
    {
        ScheduleOpeningManager.OnScheduleUIOpen += ScheduleOpeningManager_OnScheduleUIOpen;
        ScheduleOpeningManager.OnScheduleUIClose += ScheduleOpeningManager_OnScheduleUIClose;

        ScheduleOpeningManager.OnScheduleUIOpenInmediately += ScheduleOpeningManager_OnScheduleUIOpenInmediately;
        ScheduleOpeningManager.OnScheduleUICloseInmediately += ScheduleOpeningManager_OnScheduleUICloseInmediately;
    }

    private void OnDisable()
    {
        ScheduleOpeningManager.OnScheduleUIOpen -= ScheduleOpeningManager_OnScheduleUIOpen;
        ScheduleOpeningManager.OnScheduleUIClose -= ScheduleOpeningManager_OnScheduleUIClose;

        ScheduleOpeningManager.OnScheduleUIOpenInmediately -= ScheduleOpeningManager_OnScheduleUIOpenInmediately;
        ScheduleOpeningManager.OnScheduleUICloseInmediately -= ScheduleOpeningManager_OnScheduleUICloseInmediately;
    }


    private void OpenStatsInmediately()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.ResetTrigger(SHOW_TRIGGER);

        animator.Play(SHOWING_ANIMATION_NAME);
    }

    private void CloseStatsInmediately()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.ResetTrigger(SHOW_TRIGGER);

        animator.Play(HIDDEN_ANIMATION_NAME);
    }

    private void OpenStats()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void CloseStats()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }


    #region Subscriptions
    private void ScheduleOpeningManager_OnScheduleUIOpen(object sender, System.EventArgs e)
    {
        OpenStats();
    }

    private void ScheduleOpeningManager_OnScheduleUIClose(object sender, System.EventArgs e)
    {
        CloseStats();
    }

    private void ScheduleOpeningManager_OnScheduleUIOpenInmediately(object sender, System.EventArgs e)
    {
        OpenStatsInmediately();
    }

    private void ScheduleOpeningManager_OnScheduleUICloseInmediately(object sender, System.EventArgs e)
    {
        CloseStatsInmediately();
    }
    #endregion
}
