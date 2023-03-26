using Player.Inputs;
using Weapons;
using Zenject;

namespace Player.Interaction
{
    public class WeaponInteractableObject: InteractableObject
    {
        private WeaponHolder _weaponHolder;

        [Inject]
        private void Construct(WeaponHolder weaponHolder)
        {
            _weaponHolder = weaponHolder;
        }

        protected override void Interact()
        {
            var weapon = GetComponent<WeaponBase>();
            weapon.Take();
            _weaponHolder.SetNewWeapon(weapon);
        }
    }
}