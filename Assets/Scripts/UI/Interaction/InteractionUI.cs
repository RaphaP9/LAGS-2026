using System;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI tooltipText;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    public static event EventHandler<OnInteractionUIEventArgs> OnInteractionUIShow;
    public static event EventHandler<OnInteractionUIEventArgs> OnInteractionUIHide;

    public class OnInteractionUIEventArgs : EventArgs
    {
        public IInteractable interactable;
    }

    private void OnEnable()
    {
        PlayerInteraction.OnInteractableSelected += PlayerInteraction_OnInteractableSelected;
        PlayerInteraction.OnInteractableDeselected += PlayerInteraction_OnInteractableDeselected;
    }

    private void OnDisable()
    {
        PlayerInteraction.OnInteractableSelected -= PlayerInteraction_OnInteractableSelected;
        PlayerInteraction.OnInteractableDeselected -= PlayerInteraction_OnInteractableDeselected;
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

    private void SetTooltipText(IInteractable interactable) => tooltipText.text = interactable.TooltipMessage;

    #region Subscriptions
    private void PlayerInteraction_OnInteractableSelected(object sender, PlayerInteraction.OnInteractionEventArgs e)
    {
        SetTooltipText(e.interactable);
        ShowUI();
        OnInteractionUIShow?.Invoke(this, new OnInteractionUIEventArgs { interactable = e.interactable });
    }

    private void PlayerInteraction_OnInteractableDeselected(object sender, PlayerInteraction.OnInteractionEventArgs e)
    {
        HideUI();
        OnInteractionUIHide?.Invoke(this, new OnInteractionUIEventArgs { interactable = e.interactable });
    }
    #endregion
}
