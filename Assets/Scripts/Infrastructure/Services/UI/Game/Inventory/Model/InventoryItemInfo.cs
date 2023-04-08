using UI.Game.Inventory.Model;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItems/Create item info", fileName = "Item info")]
public class InventoryItemInfo: ScriptableObject
{
    public string titleTerm;
    public string descriptionTerm;
    public int maxItemsInSlot;
    public bool isStackable;
    public InventoryItemType type;
    public SecondaryInventoryItemType secondaryType;
}