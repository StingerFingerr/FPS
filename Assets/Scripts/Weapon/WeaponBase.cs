using System;
using Player.Interaction;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon
{
    public abstract class WeaponBase: MonoBehaviour, IInteractable
    {
        [Header("Aiming")]
        public Vector3 hipPosition;
        public Vector3 aimPosition;
        public float aimingSpeed = 10f;

        protected bool IsHidden;
        
        public event Action<bool> OnAiming;
        public event Action<Vector2> OnShot;
        public event Action OnReloading;
        
        private void OnAim(InputValue inputValue)
        {
            if(IsHidden)
                return;
            
            Aim(inputValue.isPressed);
        }

        private void OnReload(InputValue inputValue)
        {
            if(IsHidden)
                return;
            
            Reload();
        }

        protected virtual void Reload() => 
            OnReloading?.Invoke();
        protected void Shot(Vector2 recoil) => 
            OnShot?.Invoke(recoil);
        protected virtual void Aim(bool aim) => 
            OnAiming?.Invoke(aim);


        public abstract void Hide();
        public abstract void Show();

        public abstract void Interact();

        protected abstract void Take();
        public abstract void ThrowAway();
    }
}