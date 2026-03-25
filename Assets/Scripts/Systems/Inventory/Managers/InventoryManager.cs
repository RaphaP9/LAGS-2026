using System;
using UnityEngine;
using System.Collections.Generic;

public partial class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Lists")]
    [SerializeField] private List<InventoryObjectSO> fullInventoryObjectList;

    [Header("Runtime Filled")]
    [SerializeField] private List<InventoryObjectQuantity> inventory;

    public List<InventoryObjectQuantity> Inventory => inventory;

    public static event EventHandler<OnInventoryEventArgs> OnInventoryInitialized; 
    public static event EventHandler<OnInventoryEventArgs> OnInventoryChanged;

    public class OnInventoryEventArgs : EventArgs
    {
        public List<InventoryObjectQuantity> inventory;
    }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeInventory();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Debug.LogWarning("There is more than one InventoryManager instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeInventory()
    {
        PopulateInventory();

        foreach(InventoryObjectIDQuantity inventoryObjectIDQuantity in StaticDataManager.Instance.Data.currentInventory)
        {
            foreach(InventoryObjectQuantity inventoryObjectQuantity in inventory)
            {
                if(inventoryObjectIDQuantity.inventoryObjectID == inventoryObjectQuantity.inventoryObjectSO.id)
                {
                    inventoryObjectQuantity.quantity = inventoryObjectIDQuantity.quantity;
                    continue;
                }
            }
        }

        OnInventoryInitialized?.Invoke(this, new OnInventoryEventArgs { inventory = inventory });  
    }

    private void PopulateInventory()
    {
        foreach(InventoryObjectSO inventoryObjectSO in fullInventoryObjectList)
        {
            InventoryObjectQuantity inventoryObjectQuantity = new InventoryObjectQuantity { inventoryObjectSO = inventoryObjectSO, quantity = 0 };
            inventory.Add(inventoryObjectQuantity);
        }
    }

    public void AddInventoryObject(InventoryObjectSO inventoryObjectSO, int quantity)
    {
        foreach(InventoryObjectQuantity inventoryObjectQuantity in inventory)
        {
            if(inventoryObjectQuantity.inventoryObjectSO == inventoryObjectSO)
            {
                inventoryObjectQuantity.quantity += quantity;
            }
        }

        OnInventoryChanged?.Invoke(this, new OnInventoryEventArgs { inventory = inventory });
        StaticDataManager.Instance.SetCurrentInventory(inventory);
    }

    public void RemoveInventoryObject(InventoryObjectSO inventoryObjectSO, int quantity)
    {
        if (!HasQuantityInInventory(inventoryObjectSO, quantity)) return;

        foreach (InventoryObjectQuantity inventoryObjectQuantity in inventory)
        {
            if (inventoryObjectQuantity.inventoryObjectSO == inventoryObjectSO)
            {
                inventoryObjectQuantity.quantity = inventoryObjectQuantity.quantity - quantity < 0? 0: inventoryObjectQuantity.quantity - quantity;
            }
        }

        OnInventoryChanged?.Invoke(this, new OnInventoryEventArgs { inventory = inventory });
        StaticDataManager.Instance.SetCurrentInventory(inventory);
    }

    public bool HasQuantityInInventory(InventoryObjectSO inventoryObjectSO, int quantity)
    {
        foreach (InventoryObjectQuantity inventoryObjectQuantity in inventory)
        {
            if (inventoryObjectQuantity.inventoryObjectSO == inventoryObjectSO)
            {
                if (inventoryObjectQuantity.quantity >= quantity) return true;
                return false;
            }
        }

        return false;
    }

    public int GetQuantityOfInventoryObject(InventoryObjectSO inventoryObjectSO)
    {
        foreach (InventoryObjectQuantity inventoryObjectQuantity in inventory)
        {
            if (inventoryObjectQuantity.inventoryObjectSO == inventoryObjectSO)
            {
                return inventoryObjectQuantity.quantity;
            }
        }

        return 0;
    }
}
