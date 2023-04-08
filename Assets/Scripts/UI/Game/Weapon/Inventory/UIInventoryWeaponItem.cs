using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Weapons;

namespace UI.Game.Inventory
{
    public class UIInventoryWeaponItem: MonoBehaviour
    {
        [SerializeField] private WeaponBase weapon;
        public List<UIInventoryAttachmentSlot> attachmentSlots;

        [SerializeField] private TextMeshProUGUI ammoText;
        
        public void UpdateAmmo() => 
            ammoText.text = $"{weapon.ammoLeft}/{weapon.magazineCapacity}";
    }
}