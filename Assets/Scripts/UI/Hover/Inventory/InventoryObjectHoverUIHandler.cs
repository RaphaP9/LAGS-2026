using UnityEngine;
using System;

public class InventoryObjectHoverUIHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform rectTransformRefference;

    [Header("Runtime Filled")]
    [SerializeField] private InventoryObjectSO currentInventoryObjectSO;

    public event EventHandler<OnInventoryObjectEventArgs> OnInventoryObjectSet;

    public event EventHandler<OnInventoryObjectEventArgs> OnHoverOpening;
    public event EventHandler<OnInventoryObjectEventArgs> OnHoverClosing;

    public class OnInventoryObjectEventArgs : EventArgs
    {
        public InventoryObjectSO inventoryObjectSO;
    }

    private void OnEnable()
    {
        InventoryObjectHoverHandler.OnInventoryObjectEnter += InventoryObjectHoverHandler_OnInventoryObjectEnter;
        InventoryObjectHoverHandler.OnInventoryObjectExit += InventoryObjectHoverHandler_OnInventoryObjectExit;
    }

    private void OnDisable()
    {
        InventoryObjectHoverHandler.OnInventoryObjectEnter -= InventoryObjectHoverHandler_OnInventoryObjectEnter;
        InventoryObjectHoverHandler.OnInventoryObjectExit -= InventoryObjectHoverHandler_OnInventoryObjectExit;
    }

    #region Method Handlers
    private void HandleHoverEnter(InventoryObjectSO inventoryObjectSO, UIHoverHandler.PivotQuadrant pivotQuadrant)
    {
        if (currentInventoryObjectSO == inventoryObjectSO) return;

        GeneralUtilities.AdjustRectTransformPivotToScreenQuadrant(rectTransformRefference, pivotQuadrant.screenQuadrant, pivotQuadrant.rectTransformPoint);

        SetCurrentInventoryObject(inventoryObjectSO);
        OnInventoryObjectSet?.Invoke(this, new OnInventoryObjectEventArgs { inventoryObjectSO = inventoryObjectSO });
        OnHoverOpening?.Invoke(this, new OnInventoryObjectEventArgs { inventoryObjectSO = inventoryObjectSO });
    }

    private void HandleHoverExit(InventoryObjectSO inventoryObjectSO)
    {
        if (currentInventoryObjectSO != inventoryObjectSO) return;

        OnHoverClosing?.Invoke(this, new OnInventoryObjectEventArgs { inventoryObjectSO = inventoryObjectSO });
        ClearCurrentInventoryObject();
    }
    #endregion

    #region Get & Set
    private void SetCurrentInventoryObject(InventoryObjectSO inventoryObjectSO) => currentInventoryObjectSO = inventoryObjectSO;
    private void ClearCurrentInventoryObject() => currentInventoryObjectSO = null;
    #endregion

    private void InventoryObjectHoverHandler_OnInventoryObjectEnter(object sender, InventoryObjectHoverHandler.OnInventoryObjectHoverEventArgs e)
    {
        HandleHoverEnter(e.inventoryObjectSO, e.pivotQuadrant);
    }

    private void InventoryObjectHoverHandler_OnInventoryObjectExit(object sender, InventoryObjectHoverHandler.OnInventoryObjectHoverEventArgs e)
    {
        HandleHoverExit(e.inventoryObjectSO);

    }
}
