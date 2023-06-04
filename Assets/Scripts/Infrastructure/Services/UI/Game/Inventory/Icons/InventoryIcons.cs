using System;
using UI.Game.Inventory.Model;
using UnityEngine;

[CreateAssetMenu(menuName = "Services/ create Inventory Icons service", fileName = "InventoryIcons")]
public class InventoryIcons: ScriptableObject, IInventoryIcons
{
    public Sprite ammo;
    public Sprite shotgunAmmo;
    public Sprite medKitLittle;
    public Sprite medKitBig;
    public Sprite antidote;
    
    public Sprite silencer;
    public Sprite compensator;
    public Sprite extendedMagazine;

    public Sprite GetIcon(InventoryItemInfo info)
    {
        Sprite sprite = info.type switch
        {
            InventoryItemType.Ammo => ammo,
            InventoryItemType.ShotgunAmmo => shotgunAmmo,
            InventoryItemType.MedKitLittle => medKitLittle,
            InventoryItemType.MedKitBig => medKitBig,
            InventoryItemType.Antidote => antidote,
            InventoryItemType.Attachment => null,
            _ => null
        };

        if (sprite is not null)
            return sprite;

        sprite = info.secondaryType switch
        {
            SecondaryInventoryItemType.Silencer => silencer,
            SecondaryInventoryItemType.Compensator => compensator,
            SecondaryInventoryItemType.ExtendedMagazine => extendedMagazine,
            _ => null
        };

        if (sprite is null)
            throw new Exception("Icon not founded");
        
        return sprite;
    }
}