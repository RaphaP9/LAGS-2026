using UnityEngine;
using System;

public class SimpleDialogueNPCInteractable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    [SerializeField, Range(1f, 100f)] private float interactionRange;
    [SerializeField] private string tooltipMessage;
    [Space]
    [SerializeField] private DialogueSO dialogueSO;

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
        DialogueManager.Instance.StartDialogue(dialogueSO); 
        OnInteractableInteracted?.Invoke(this, EventArgs.Empty);      
    }

    public void Select()
    {
        OnInteractableSelected?.Invoke(this, EventArgs.Empty);
    }

    public void Deselect()
    {
        OnInteractableDeselected?.Invoke(this, EventArgs.Empty);
    }

    public Transform GetTransform() => transform;
    #endregion
}
