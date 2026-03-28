using System.Collections;
using UnityEngine;

public class MinigameKeybindIndicatorUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField, Range(0f, 10f)] private float timeToShow;
    [SerializeField, Range(0f, 10f)] private float timeToStartMoving;
    [SerializeField, Range(0f, 10f)] private float timeBetweenMoves;
    [SerializeField, Range(0f, 10f)] private float timeToHide;

    private const string SHOW_TRIGGER = "Show";
    private const string MOVE_TRIGGER = "Move";
    private const string HIDE_TRIGGER = "Hide";

    protected void Show()
    {
        StartCoroutine(ShowCoroutine());
    }

    protected void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        yield return new WaitForSeconds(timeToShow);
        ShowUI();
        yield return new WaitForSeconds(timeToStartMoving);
        
        while (true)
        {
            MoveUI();
            yield return new WaitForSeconds(timeBetweenMoves);
        }
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(timeToHide);
        HideUI();
    }

    #region Animations

    private void ShowUI()
    {
        animator.ResetTrigger(MOVE_TRIGGER);
        animator.ResetTrigger(HIDE_TRIGGER);

        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void MoveUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(HIDE_TRIGGER);

        animator.SetTrigger(MOVE_TRIGGER);
    }

    private void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(MOVE_TRIGGER);

        animator.SetTrigger(HIDE_TRIGGER);
    }

    #endregion
}
