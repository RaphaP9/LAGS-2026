using System.Collections;
using TMPro;
using UnityEngine;

public class DayStartUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI dayText;

    [Header("Settings")]
    [SerializeField, Range(0f, 5f)] private float timeToShow;
    [SerializeField, Range(1f, 10f)] private float timeShowing;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private void OnEnable()
    {
        DayStartManager.OnDayStart += DayStartManager_OnDayStart;
    }

    private void OnDisable()
    {
        DayStartManager.OnDayStart -= DayStartManager_OnDayStart;
    }

    private IEnumerator DayStartCoroutine(int day)
    {
        dayText.text = day.ToString();

        yield return new WaitForSeconds(timeToShow);
        ShowUI();
        yield return new WaitForSeconds(timeShowing);
        HideUI();
    }

    private void ShowUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }

    private void DayStartManager_OnDayStart(object sender, DayStartManager.OnDayStartEventArgs e)
    {
        StartCoroutine(DayStartCoroutine(e.day));
    }
}
