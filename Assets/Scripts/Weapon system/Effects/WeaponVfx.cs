using UnityEngine;
using Weapons;

namespace Effects
{
    public class WeaponVfx: MonoBehaviour
    {
        public WeaponBase weapon;
        public Transform effectRoot;
        public GameObject effectPrefab;

        private void OnEnable() => 
            weapon.OnShot += PlayEffect;

        private void OnDisable() => 
            weapon.OnShot -= PlayEffect;
        
        private void PlayEffect(Vector2 recoil)
        {
            var effect = Instantiate(effectPrefab, effectRoot);
        }
    }
}