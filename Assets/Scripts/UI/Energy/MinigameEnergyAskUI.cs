using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameEnergyAskUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Button backButton;
    [SerializeField] private Button spendEnergyButton;
    [SerializeField] private TextMeshProUGUI spendEnergyQuantityText;

    [Header("Settings")]
    [SerializeField] private string backScene;
    [SerializeField] private TransitionType backTransitionType;

    public event EventHandler<OnEnergySpentInUIEventArgs> OnEnergySpentInUI;
    private int energyCost = 0;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    public class OnEnergySpentInUIEventArgs : EventArgs
    {
        public int energy;
    }

    private void Awake()
    {
        InitializeButtonsListeners();
    }

    private void InitializeButtonsListeners()
    {
        backButton.onClick.AddListener(LoadBackScene);
        spendEnergyButton.onClick.AddListener(SpendEnergy);
    }

    private void LoadBackScene()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(backScene, backTransitionType);
    }

    private void SpendEnergy()
    {
        if (!EnergyManager.Instance.CanSpendEnergy(energyCost)) return;

        EnergyManager.Instance.SpendEnergy(energyCost);

        OnEnergySpentInUI?.Invoke(this, new OnEnergySpentInUIEventArgs { energy = energyCost});
    }

    public void ShowUI(int energyCost)
    {
        this.energyCost = energyCost;

        spendEnergyQuantityText.text = energyCost.ToString();

        if (!EnergyManager.Instance.CanSpendEnergy(energyCost)) spendEnergyButton.enabled = false;
        else spendEnergyButton.enabled = true;

        animator.ResetTrigger(HIDE_TRIGGER);
        animator.SetTrigger(SHOW_TRIGGER);
    }

    public void HideUI()
    {
        animator.ResetTrigger(SHOW_TRIGGER);
        animator.SetTrigger(HIDE_TRIGGER);
    }
}
