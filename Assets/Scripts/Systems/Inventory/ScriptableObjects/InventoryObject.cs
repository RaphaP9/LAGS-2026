using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryObjectSO", menuName = "ScriptableObjects/Inventory/InventoryObject")]
public class InventoryObjectSO : ScriptableObject
{
    public int id;
    public string objectName;
    [TextArea(3,10)] public string description;
    public Sprite sprite;
}
