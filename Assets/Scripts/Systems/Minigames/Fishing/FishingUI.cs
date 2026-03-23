using System;
using UnityEngine;

public class FishingUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private RectTransform fillHolder;
    [SerializeField] private RectTransform indicatorHolder;

    [Header("Settings")]
    [SerializeField, Range(0f, 1f)] private float indicatorInpulse;
    [Space]
    [SerializeField, Range(1, 10)] private int minTilts;
    [SerializeField, Range(1, 10)] private int maxTilts;
    [Space]
    [SerializeField, Range(0f, 1f)] private int safeZoneWidth;
    [Space]
    [SerializeField, Range(1f, 10f)] private float minSafeZoneStayTime; 
    [SerializeField, Range(1f, 10f)] private float maxSafeZoneStayTime;
    [Space]
    [SerializeField, Range(1f, 10f)] private float minOutStayTime;
    [SerializeField, Range(1f, 10f)] private float maxOutStayTime;
    [Space]
    [SerializeField] private float minPositionX;
    [SerializeField] private float maxPositionX;


    private const string SHOW_TRIGGER = "Show";
    private const string SUCCESS_TRIGGER = "Success";
    private const string FAIL_TRIGGER = "Fail";

    public event EventHandler OnFishingSuccess;
    public event EventHandler OnFishingFail;

    #region Animations
    public void ShowUI()
    {
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void SuccessUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(FAIL_TRIGGER);
        animator.SetTrigger(SUCCESS_TRIGGER);
    }

    public void FailUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.ResetTrigger(SUCCESS_TRIGGER);
        animator.SetTrigger(FAIL_TRIGGER);
    }
    #endregion
}
