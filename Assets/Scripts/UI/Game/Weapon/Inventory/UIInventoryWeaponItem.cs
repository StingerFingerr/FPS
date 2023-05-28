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

        private void OnEnable()
        {
            if (weapon.attachmentSystem is null)
                return;
            weapon.attachmentSystem.OnModuleChanged += UpdateAmmo;
        }

        private void OnDisable()
        {
            if (weapon.attachmentSystem is null)
                return;
            weapon.attachmentSystem.OnModuleChanged -= UpdateAmmo;
        }

        public void UpdateAmmo() => 
            ammoText.text = $"{weapon.ammoLeft}/{weapon.MagazineCapacity}";
    }
}