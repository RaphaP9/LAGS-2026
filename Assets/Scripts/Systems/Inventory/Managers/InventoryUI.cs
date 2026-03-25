using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<SingleInventoryObjectInventoryObjectSO> relationships;

    [System.Serializable]
    public class SingleInventoryObjectInventoryObjectSO
    {
        public SingleInventoryObjectUI singleInventoryObjectUI;
        public InventoryObjectSO inventoryObjectSO;
    }

    private void OnEnable()
    {
        InventoryManager.OnInventoryInitialized += InventoryManager_OnInventoryInitialized;
        InventoryManager.OnInventoryChanged += InventoryManager_OnInventoryChanged;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryInitialized -= InventoryManager_OnInventoryInitialized;
        InventoryManager.OnInventoryChanged -= InventoryManager_OnInventoryChanged;
    }

    private void SetUIs()
    {
        foreach(SingleInventoryObjectInventoryObjectSO relation in relationships)
        {
            relation.singleInventoryObjectUI.SetUI(relation.inventoryObjectSO);
        }
    }

    private void UpdateUIs()
    {
        foreach (SingleInventoryObjectInventoryObjectSO relation in relationships)
        {
            relation.singleInventoryObjectUI.UpdateQuantity();
        }
    }

    private void InventoryManager_OnInventoryInitialized(object sender, InventoryManager.OnInventoryEventArgs e)
    {
        SetUIs();
    }

    private void InventoryManager_OnInventoryChanged(object sender, InventoryManager.OnInventoryEventArgs e)
    {
        UpdateUIs();
    }
}
