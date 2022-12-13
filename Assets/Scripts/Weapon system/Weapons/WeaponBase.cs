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

        public bool isHidden;
        
        public event Action<bool> OnAiming;
        public event Action<Vector2> OnShot;
        public event Action OnReloading;
        
        private void OnAim(InputValue inputValue)
        {
            if(isHidden)
                return;
            
            Aim(inputValue.isPressed);
        }

        private void OnReload(InputValue inputValue)
        {
            if(isHidden)
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