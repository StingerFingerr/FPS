using UnityEngine;
using Weapons;
using Zenject;

namespace Game_logic.Spawners
{
    public class WeaponSpawner: MonoBehaviour
    {
        public string weaponName;

        private WeaponBase.Factory _weaponFactory;
        
        [Inject]
        private void Construct(WeaponBase.Factory weaponFactory) => 
            _weaponFactory = weaponFactory;

        public void Spawn()
        {
            var weapon = _weaponFactory.Create(weaponName);
            weapon.transform.position = transform.position;
        }
    }
}