using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    [Header("Enabler")]
    [SerializeField] private bool interactionEnabled;

    [Header("Components")]
    [SerializeField] private InteractionInput interactionInput;

    [Header("Interaction Settings")]
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField, Range(1f, 5f)] private float interactionSphereRadius;

    [Header("Debug")]
    [SerializeField] private bool drawRaycasts;

    private bool InteractionDownInput => interactionInput.GetInteractionDown();
    private bool CanProcessInteractionInput => interactionInput.CanProcessInteractionInput();
    public bool IsInteracting { get; private set; }
    public bool InteractionEnabled => interactionEnabled;
    private IInteractable currentInteractable;
    public IInteractable CurrentInteractable => currentInteractable;

    public static event EventHandler<OnInteractionEventArgs> OnInteractableSelected;
    public static event EventHandler<OnInteractionEventArgs> OnInteractableDeselected;

    public static event EventHandler<OnInteractionEventArgs> OnInteraction;

    public class OnInteractionEventArgs : EventArgs
    {
        public IInteractable interactable;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Update()
    {
        HandleInteractableSelections();
        HandleInteractions();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one PlayerInteraction instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void HandleInteractableSelections()
    {
        IInteractable interactable = CheckForInteractable();

        if (!CanInteract()) interactable = null;

        if (interactable != null)
        {
            if (currentInteractable == null)
            {
                SelectInteractable(interactable);
            }
            else if (currentInteractable != interactable)
            {
                DeselectInteractable(currentInteractable);
                SelectInteractable(interactable);
            }
        }
        else if (currentInteractable != null)
        {
            DeselectInteractable(currentInteractable);
        }
    }

    private void SelectInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;

        interactable.Select();
        OnInteractableSelected?.Invoke(this, new OnInteractionEventArgs { interactable = interactable });
    }

    private void DeselectInteractable(IInteractable interactable)
    {
        currentInteractable = null;

        interactable.Deselect();
        OnInteractableDeselected?.Invoke(this, new OnInteractionEventArgs { interactable = interactable });
    }

    #region Interactable Selection
    private IInteractable CheckForInteractable()
    {
        Collider[] colliderHits = GetInteractableLayerHitsNormal();

        if (colliderHits.Length <= 0) return null;

        IInteractable closestInteractable = null;
        float closestInteractableDistance = float.MaxValue;

        foreach (Collider collider in colliderHits)
        {
            if (GeneralUtilities.TryGetGenericFromTransform<IInteractable>(collider.transform, out var interactable))
            {
                if (!interactable.IsSelectable) continue;
                if (!InteractableInRange(interactable)) continue;
                
                float currentInteractableDistance = GetDistanceToInteractable(interactable);

                if(currentInteractableDistance < closestInteractableDistance)
                {
                    closestInteractable = interactable;
                    closestInteractableDistance = currentInteractableDistance;
                }
            }
        }

        if (closestInteractable == null) return null;

        return closestInteractable;
    }

    public bool InteractableInRange(IInteractable interactable)
    {
        if(Vector3.Distance(transform.position, interactable.GetTransform().position) > interactable.InteractionRange) return false;
        return true;
    }

    public float GetDistanceToInteractable(IInteractable interactable) => Vector3.Distance(transform.position, interactable.GetTransform().position);

    public Collider[] GetInteractableLayerHitsNormal()
    {
        Collider[] hits = Physics.OverlapSphere(GetRaycastOrigin(), interactionSphereRadius, interactionLayer);
        return hits;
    }

    #endregion

    #region Interactable Interaction
    private void HandleInteractions()
    {
        if (!CanInteract()) return;
        if (currentInteractable == null) return;

        if (InteractionDownInput)
        {
            currentInteractable.TryInteract();
            OnInteraction?.Invoke(this, new OnInteractionEventArgs { interactable = currentInteractable});
        }
    }
    #endregion

    public Vector3 GetRaycastOrigin() => transform.position;

    private bool CanInteract()
    {
        if (!CanProcessInteractionInput) return false;
        if (!interactionEnabled) return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        if (drawRaycasts)
        {
            Gizmos.DrawSphere(GetRaycastOrigin(), interactionSphereRadius);
            Gizmos.color = Color.yellow;
        }
    }
}
