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
        
        [Inject]
        private void Construct()
        {


            _uniqueId ??= GetComponent<UniqueId>();
        }
        
        public void Load(Progress progress)
        {
            if (progress.WeaponInfos.ContainsKey(_uniqueId.id))
                Debug.Log(progress.WeaponInfos[_uniqueId.id]?.name);
            else Debug.Log("null");
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