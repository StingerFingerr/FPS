using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Services/ create Inventory Icons service", fileName = "InventoryIcons")]
public class InventoryIcons: ScriptableObject, IInventoryIcons
{
    public Sprite ammo;
    public Sprite medKitLittle;
    public Sprite medKitBig;

    public Sprite GetIcon(InventoryItemType type)
    {
        return type switch
        {
            InventoryItemType.Ammo => ammo,
            InventoryItemType.MedKitLittle => medKitLittle,
            InventoryItemType.MedKitBig => medKitBig,
            _ => throw new Exception("Icon not founded")
        };
    }
}