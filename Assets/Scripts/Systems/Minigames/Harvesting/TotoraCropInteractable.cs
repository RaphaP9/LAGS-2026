using System;
using UnityEngine;

public class TotoraCropInteractable : MonoBehaviour, IInteractable
{
    [Header("Component")]
    [SerializeField] private TotoraCropHandler totoraCropHandler;

    [Header("Interactable Settings")]
    [SerializeField, Range(1f, 100f)] private float interactionRange;
    [SerializeField] private string tooltipMessage;

    public event EventHandler OnTotoraCropHarvested;
    public static event EventHandler OnAnyTotoraCropHarvested;

    #region IInteractable Properties
    public float InteractionRange => interactionRange;
    public bool IsSelectable => CheckCanBeSelected();
    public string TooltipMessage => tooltipMessage;
    #endregion

    #region IInteractableEvents
    public event EventHandler OnInteractableSelected;
    public event EventHandler OnInteractableDeselected;
    public event EventHandler OnInteractableInteracted;
    #endregion

    #region  IInteractable Methods

    private bool CheckCanBeSelected()
    {
        if(GameManager.Instance.GameState != GameManager.State.Exploring) return false;
        if(totoraCropHandler.IsHarvested) return false;

        if(!EnergyManager.Instance.CanSpendEnergy(ActivitiesManager.Instance.HarvestingEnergyCost)) return false;

        return true;
    }

    public void Interact()
    {
        OnInteractableInteracted?.Invoke(this, EventArgs.Empty);
        totoraCropHandler.HarvestTotoraCrop();
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
