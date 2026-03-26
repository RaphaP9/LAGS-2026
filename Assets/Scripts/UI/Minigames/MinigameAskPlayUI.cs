using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameAskPlayUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Button playButton;
    [SerializeField] private Button backButton;

    [Header("Settings")]
    [SerializeField] private string backScene;
    [SerializeField] private TransitionType backTransitionType;

    public event EventHandler OnMinigamePlay;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        backButton.onClick.AddListener(LoadBackScene);
        playButton.onClick.AddListener(PlayMinigame);
    }

    private void LoadBackScene()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(backScene, backTransitionType);
    }

    private void PlayMinigame()
    {
        OnMinigamePlay?.Invoke(this, EventArgs.Empty);
    }

    public void ShowUI()
    {
        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }
}
