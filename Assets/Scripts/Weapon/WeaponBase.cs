using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon
{
    public abstract class WeaponBase: MonoBehaviour
    {
        [Header("Aiming")]
        public Vector3 hipPosition;
        public Vector3 aimPosition;
        public float aimingSpeed = 10f;
        
        public event Action<bool> OnAiming;
        public event Action<Vector2> OnShot;
        public event Action OnReloading;
        
        private void OnAim(InputValue inputValue)
        {
            Aim(inputValue.isPressed);
        }

        private void OnFire(InputValue inputValue)
        {
            Shot(Vector2.zero);
        }

        private void OnReload(InputValue inputValue)
        {
            Reload();
        }
        
        protected virtual void Reload() => 
            OnReloading?.Invoke();
        protected virtual void Shot(Vector2 recoil) => 
            OnShot?.Invoke(recoil);
        protected virtual void Aim(bool aim) => 
            OnAiming?.Invoke(aim);


        public abstract void Hide();
        public abstract void Show();
    }
}