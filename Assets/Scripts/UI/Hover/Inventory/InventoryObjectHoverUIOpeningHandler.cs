using UnityEngine;

public class InventoryObjectHoverUIOpeningHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InventoryObjectHoverUIHandler inventoryObjectHoverUI;
    [SerializeField] private Animator animator;

    private const string HOVER_IN_TRIGGER = "HoverIn";
    private const string HOVER_OUT_TRIGGER = "HoverOut";

    private void OnEnable()
    {
        inventoryObjectHoverUI.OnHoverOpening += InventoryObjectHoverUI_OnHoverOpening;
        inventoryObjectHoverUI.OnHoverClosing += InventoryObjectHoverUI_OnHoverClosing;
    }

    private void OnDisable()
    {
        inventoryObjectHoverUI.OnHoverOpening -= InventoryObjectHoverUI_OnHoverOpening;
        inventoryObjectHoverUI.OnHoverClosing -= InventoryObjectHoverUI_OnHoverClosing;
    }

    private void HoverIn()
    {
        animator.ResetTrigger(HOVER_OUT_TRIGGER);
        animator.SetTrigger(HOVER_IN_TRIGGER);
    }

    private void HoverOut()
    {
        animator.ResetTrigger(HOVER_IN_TRIGGER);
        animator.SetTrigger(HOVER_OUT_TRIGGER);
    }

    private void InventoryObjectHoverUI_OnHoverOpening(object sender, InventoryObjectHoverUIHandler.OnInventoryObjectEventArgs e)
    {
        HoverIn();
    }

    private void InventoryObjectHoverUI_OnHoverClosing(object sender, InventoryObjectHoverUIHandler.OnInventoryObjectEventArgs e)
    {
        HoverOut();
    }
}
