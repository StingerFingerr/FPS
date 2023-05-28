using UnityEngine;
using Weapons;
using Zenject;

namespace UI.Game.Inventory
{
    public class UIInventoryWeaponSlot: MonoBehaviour
    {
        public Transform inventorySlots;

        private PlayerInputs _playerInput;
        private WeaponBase _weapon;


        [Inject]
        private void Construct(PlayerInputs playerInput) => 
            _playerInput = playerInput;


        private void OnEnable() => 
            _playerInput.onTab += UpdateSlot;

        private void OnDisable() => 
            _playerInput.onTab -= UpdateSlot;

        private void UpdateSlot() => 
            _weapon?.uiInventoryWeaponItem.UpdateAmmo();

        public void SetWeapon(WeaponBase weapon)
        {
            Clear();
            _weapon = weapon;

            if (_weapon is null)
                return;

            Transform itemTransform = weapon.uiInventoryWeaponItem.transform;
            itemTransform.SetParent(transform);
            itemTransform.localPosition = Vector3.zero;

            _weapon.uiInventoryWeaponItem.transform.localEulerAngles = Vector3.zero;
            _weapon.uiInventoryWeaponItem.gameObject.SetActive(true);
            _weapon.uiInventoryWeaponItem.attachmentSlots.ForEach(slot =>
            {
                slot.transform.localEulerAngles = Vector3.zero;
                slot.transform.SetParent(inventorySlots);
                slot.gameObject.SetActive(true);
            });
        }

        public void Clear()
        {
            if (_weapon is null)
                return;
            
            _weapon.uiInventoryWeaponItem.gameObject.SetActive(false);
            _weapon.uiInventoryWeaponItem.attachmentSlots.ForEach(slot =>
            {
                slot.transform.SetParent(_weapon.uiInventoryWeaponItem.transform);
                slot.gameObject.SetActive(false);
            });
            _weapon = null;
        }
    }
}