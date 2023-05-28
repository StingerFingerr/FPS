using UnityEngine;
using Weapons;
using Zenject;

namespace UI.Game.Inventory
{
    public class UIInventoryWeaponHolder: MonoBehaviour
    {
        [SerializeField] private UIInventoryWeaponSlot[] slots;
        private WeaponHolder _weaponHolder;

        [Inject]
        private void Construct(WeaponHolder weaponHolder) => 
            _weaponHolder = weaponHolder;

        private void OnEnable() => 
            _weaponHolder.OnWeaponSwitched += SetWeapon;

        private void OnDisable() => 
            _weaponHolder.OnWeaponSwitched -= SetWeapon;

        public void SetWeapon(WeaponBase weapon, int index)
        {
            if (weapon is null)
                slots[index].Clear();
            else slots[index].SetWeapon(weapon);
        }
        
    }
}