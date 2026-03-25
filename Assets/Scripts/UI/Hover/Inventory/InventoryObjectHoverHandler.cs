using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventoryObjectHoverHandler : UIHoverHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Components")]
    [SerializeField] private SingleInventoryObjectUI singleInventoryObjectUI;

    [Header("Runtime Filled")]
    [SerializeField] private bool isHovered;

    public bool IsHovered => isHovered;

    public static event EventHandler<OnInventoryObjectHoverEventArgs> OnInventoryObjectEnter;
    public static event EventHandler<OnInventoryObjectHoverEventArgs> OnInventoryObjectExit;

    public class OnInventoryObjectHoverEventArgs : EventArgs
    {
        public InventoryObjectSO inventoryObjectSO;
        public PivotQuadrant pivotQuadrant;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PivotQuadrant pivotQuadrant = GetPivotQuadrantByScreenQuadrant(GeneralUtilities.GetScreenQuadrant(rectTransformRefference));
        isHovered = true;
        OnInventoryObjectEnter?.Invoke(this, new OnInventoryObjectHoverEventArgs { inventoryObjectSO = singleInventoryObjectUI.InventoryObjectSO, pivotQuadrant = pivotQuadrant });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        OnInventoryObjectExit?.Invoke(this, new OnInventoryObjectHoverEventArgs { inventoryObjectSO = singleInventoryObjectUI.InventoryObjectSO });
    }
}
