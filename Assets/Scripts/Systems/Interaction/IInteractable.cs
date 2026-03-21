using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IInteractable 
{
    public event EventHandler OnInteractableSelected;
    public event EventHandler OnInteractableDeselected;

    public event EventHandler OnInteractableInteracted;
    public event EventHandler OnInteractableFailInteracted;

    public float InteractionRange { get; }

    public bool IsSelectable { get; }
    public bool IsInteractable { get; }
    public string TooltipMessage { get; }

    public void Select();
    public void Deselect();
    public void TryInteract();
    public void Interact();
    public void FailInteract();
    public Transform GetTransform();
}
