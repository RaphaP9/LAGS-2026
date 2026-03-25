using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleInventoryObjectUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image objectImage;
    [SerializeField] private TextMeshProUGUI quantityText;

    [Header("Settings")]
    [SerializeField] private InventoryObjectSO inventoryObjectSO;

    public InventoryObjectSO InventoryObjectSO => inventoryObjectSO;

    public void SetUI(InventoryObjectSO inventoryObjectSO)
    {
        this.inventoryObjectSO = inventoryObjectSO;
        objectImage.sprite = inventoryObjectSO.sprite;

        UpdateQuantity();
    }

    public void UpdateQuantity()
    {
        if (inventoryObjectSO == null) return;
        quantityText.text = InventoryManager.Instance.GetQuantityOfInventoryObject(inventoryObjectSO).ToString();
    }
}
