using System.Collections.Generic;
using Game_logic;
using Infrastructure;
using UnityEngine;
using Weapons;
using Zenject;

[RequireComponent(typeof(UniqueId))]
public class WeaponSlot: MonoBehaviour, IProgressReader, IProgressWriter
{
    public WeaponBase weapon;

    private UniqueId _uniqueId;
    private WeaponBase.Factory _weaponFactory;
        
    [Inject]
    private void Construct(WeaponBase.Factory weaponFactory)
    {
        _weaponFactory = weaponFactory;

        _uniqueId ??= GetComponent<UniqueId>();
    }
        
    public void Load(Progress progress)
    {
        WeaponInfo info = progress.WeaponInfos[_uniqueId.id];

        if (info is not null)
        {
            weapon = _weaponFactory.Create(info.name);

            weapon.name = info.name;
            weapon.isHidden = info.isHidden;
            weapon.transform.parent = transform;
            weapon.transform.localEulerAngles = Vector3.zero;
            weapon.ammoLeft = info.ammoLeft;
                
            weapon.Take();
            if (info.isHidden)
            {
                weapon.transform.localPosition = weapon.hipPosition;
                weapon.Hide();
            }
            else
            {
                weapon.Show();
            }


            List<InventoryItemInfo> attachmentItems = info.attachmentItems;
            for (int i = 0; i < attachmentItems.Count; i++)
            {
                
                if(attachmentItems[i] is not null)
                    weapon.uiInventoryWeaponItem.attachmentSlots[i].SetModule(attachmentItems[i]);
            }
        }
    }

    public void Save(Progress progress)
    {
        WeaponInfo info = null;

        if (weapon)
        {
            List<InventoryItemInfo> attachmentItems = new List<InventoryItemInfo>();
            foreach (var slot in weapon.uiInventoryWeaponItem.attachmentSlots)
                attachmentItems.Add(slot.draggableItem.ItemInfo);

            info = new()
            {
                name = weapon.name,
                isHidden = weapon.isHidden,
                attachmentItems = attachmentItems,
                ammoLeft = weapon.ammoLeft
            };
        }
            

        if (progress.WeaponInfos.TryAdd(_uniqueId.id, info) is false)
            progress.WeaponInfos[_uniqueId.id] = info;
    }
}