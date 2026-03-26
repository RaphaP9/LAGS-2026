using UnityEngine;
using UnityEngine.UI;


public class FinalDayEndUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Button finalDayButton;

    [Header("Settingas")]
    [SerializeField] private string finalDayScene;
    [SerializeField] private TransitionType finalDayTransitionType;

    private const string SHOW_TRIGGER = "Show";

    private void OnEnable()
    {
        DayTimeManager.OnDayEnd += DayTimeManager_OnDayEnd;
    }

    private void OnDisable()
    {
        DayTimeManager.OnDayEnd -= DayTimeManager_OnDayEnd;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        finalDayButton.onClick.AddListener(FinalDay);
    }

    private void FinalDay()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(finalDayScene, finalDayTransitionType);
    }

    private void ShowUI()
    {
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void DayTimeManager_OnDayEnd(object sender, DayTimeManager.OnDayEventArgs e)
    {
        if (DayTimeManager.Instance.IsLastDay()) ShowUI();
    }
}
