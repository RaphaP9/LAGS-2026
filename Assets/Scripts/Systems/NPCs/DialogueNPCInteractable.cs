using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class DialogueNPCInteractable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    [SerializeField, Range(1f, 100f)] private float interactionRange;
    [SerializeField] private string tooltipMessage;
    
    [Header("Dialogues")]
    [SerializeField] private DialogueSO firstInteractionDialogue;
    [SerializeField] private List<DialogueDayRelation> dialogueDayRelations;

    [System.Serializable]
    public class DialogueDayRelation
    {
        [Range(1,4)] public int day;
        public DialogueSO dialogueSO;

    }

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
        DialogueManager.Instance.StartDialogue(GetDialogueToPlay()); 
        OnInteractableInteracted?.Invoke(this, EventArgs.Empty);

        IncreaseTimesInteractedNPC();
    }

    public void Select()
    {
        OnInteractableSelected?.Invoke(this, EventArgs.Empty);
    }

    public void Deselect()
    {
        OnInteractableDeselected?.Invoke(this, EventArgs.Empty);
    }

    protected abstract bool IsFirstTimeInteracting();
    protected abstract void IncreaseTimesInteractedNPC();

    private DialogueSO GetDialogueToPlay()
    {
        if (IsFirstTimeInteracting()) return firstInteractionDialogue;
        else return GetDialogueByDay(DayTimeManager.Instance.CurrentDay);
    }

    private DialogueSO GetDialogueByDay(int day)
    {
        foreach(DialogueDayRelation relation in dialogueDayRelations)
        {
            if (relation.day == day) return relation.dialogueSO;
        }

        return dialogueDayRelations[^1].dialogueSO;
    }

    public Transform GetTransform() => transform;
    #endregion
}
