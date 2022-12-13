using Game_logic;
using Infrastructure;
using UnityEngine;
using Weapon;
using Zenject;

namespace Weapon_system
{
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

                if (info.isHidden)
                {
                    weapon.transform.localPosition = weapon.hiddenPosition;
                    weapon.Hide();
                }
                else
                    weapon.Show();
            }
        }

        public void Save(Progress progress)
        {
            WeaponInfo info = null;

            if (weapon)
                info = new()
                {
                    name = weapon.name,
                    isHidden = weapon.isHidden
                };

            if (progress.WeaponInfos.TryAdd(_uniqueId.id, info) is false)
                progress.WeaponInfos[_uniqueId.id] = info;
        }
    }
}