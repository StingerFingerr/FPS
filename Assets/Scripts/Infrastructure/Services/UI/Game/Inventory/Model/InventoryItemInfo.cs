using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItems/Create item info", fileName = "Item info")]
public class InventoryItemInfo: ScriptableObject
{
    public string title;
    public string description;
    public int maxItemsInSlot;
    public bool isStackable;
    public InventoryItemType type;
}