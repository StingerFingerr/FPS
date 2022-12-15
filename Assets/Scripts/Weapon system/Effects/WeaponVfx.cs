using UnityEngine;

namespace Weapon.Effects
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
        
        private void PlayEffect(Vector2 obj)
        {
            var effect = Instantiate(effectPrefab, effectRoot);
        }
    }
}