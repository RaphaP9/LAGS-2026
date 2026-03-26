using UnityEngine;
using UnityEngine.UI;

public class DayEndUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Button nextDayButton;

    [Header("Settingas")]
    [SerializeField] private string nextDayScene;
    [SerializeField] private TransitionType nextDayTransitionType;

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
        nextDayButton.onClick.AddListener(NextDay);
    }

    private void NextDay()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(nextDayScene, nextDayTransitionType);  
    }

    private void ShowUI()
    {
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void DayTimeManager_OnDayEnd(object sender, DayTimeManager.OnDayEventArgs e)
    {
        if (!DayTimeManager.Instance.IsLastDay()) ShowUI();
    }
}
