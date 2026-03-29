using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryObjectHoverUIContentsHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InventoryObjectHoverUIHandler inventoryObjectHoverUI;

    [Header("UI Components")]
    [SerializeField] private Image inventoryObjectImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private TextMeshProUGUI inventoryObjectNameText;
    [SerializeField] private TextMeshProUGUI inventoryObjectDescriptionText;
    [SerializeField] private TextMeshProUGUI quantityText;

    private void OnEnable()
    {
        inventoryObjectHoverUI.OnInventoryObjectSet += InventoryObjectHoverUI_OnInventoryObjectSet;
    }

    private void OnDisable()
    {
        inventoryObjectHoverUI.OnInventoryObjectSet -= InventoryObjectHoverUI_OnInventoryObjectSet;
    }

    private void CompleteSetUI(InventoryObjectSO inventoryObjectSO)
    {
        SetNameText(inventoryObjectSO);
        SetImage(inventoryObjectSO);
        SetBorderImage(inventoryObjectSO);
        SetDescriptionText(inventoryObjectSO);
        SetQuantityText(inventoryObjectSO);
    }

    private void SetNameText(InventoryObjectSO inventoryObjectSO) => inventoryObjectNameText.text = inventoryObjectSO.objectName;
    private void SetImage(InventoryObjectSO inventoryObjectSO) => inventoryObjectImage.sprite = inventoryObjectSO.sprite;
    private void SetBorderImage(InventoryObjectSO inventoryObjectSO) => borderImage.sprite = inventoryObjectSO.borderSprite;
    private void SetDescriptionText(InventoryObjectSO inventoryObjectSO) => inventoryObjectDescriptionText.text = inventoryObjectSO.description;
    private void SetQuantityText(InventoryObjectSO inventoryObjectSO) => quantityText.text = InventoryManager.Instance.GetQuantityOfInventoryObject(inventoryObjectSO).ToString();


    private void InventoryObjectHoverUI_OnInventoryObjectSet(object sender, InventoryObjectHoverUIHandler.OnInventoryObjectEventArgs e)
    {
        CompleteSetUI(e.inventoryObjectSO);
    }
}
