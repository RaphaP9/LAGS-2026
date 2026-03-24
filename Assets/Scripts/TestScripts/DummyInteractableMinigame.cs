using UnityEngine;
using System;

public class DummyInteractableMinigame : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    [SerializeField, Range(1f, 100f)] private float interactionRange;
    [Space]
    [SerializeField] private bool canBeSelected;
    [Space]
    [SerializeField] private string tooltipMessage;
    [Space]
    [SerializeField] private string minigameScene;
    [SerializeField] private TransitionType minigameSceneTransitionType;

    #region IInteractable Properties
    public float InteractionRange => interactionRange;
    public bool IsSelectable => GameManager.Instance.GameState == GameManager.State.Exploring;
    public string TooltipMessage => tooltipMessage;
    #endregion

    #region IInteractableEvents
    public event EventHandler OnInteractableSelected;
    public event EventHandler OnInteractableDeselected;
    public event EventHandler OnInteractableInteracted;
    #endregion

    #region  IInteractable Methods

    public void Interact()
    {
        ScenesManager.Instance.TransitionLoadTargetScene(minigameScene, minigameSceneTransitionType);
        OnInteractableInteracted?.Invoke(this, EventArgs.Empty);
    }

    public void Select()
    {
        Debug.Log("DummySelected");
        OnInteractableSelected?.Invoke(this, EventArgs.Empty);
    }

    public void Deselect()
    {
        Debug.Log("DummyDeselected");
        OnInteractableDeselected?.Invoke(this, EventArgs.Empty);
    }

    public Transform GetTransform() => transform;
    #endregion
}
