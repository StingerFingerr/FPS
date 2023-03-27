using System;
using Animation;
using Player.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;
using Weapon.Recoil;
using Weapon.Sway;
using Zenject;
using Random = UnityEngine.Random;

namespace Weapons
{
    public abstract class WeaponBase: MonoBehaviour
    {
        public class Factory: PlaceholderFactory<string, WeaponBase>
        { }

        public new string name;
        public CrosshairType crosshairType;

        [Header("Aiming")]
        public Vector3 hipPosition;
        public Vector3 aimPosition;
        public float aimingSpeed = 10f;

        public WeaponAnimator animator;

        public RecoilParameters recoil;

        public BoxCollider interactableCollider;

        public WeaponSway sway;

        public Rigidbody rigidBody;

        public Sprite icon;

        public bool allowRun = true;
        
        [HideInInspector]public bool isHidden;
        
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


        public virtual void Hide()
        {
            isHidden = true;
            animator.Hide();
            enabled = false;
        }

        public virtual void Show()
        {
            isHidden = false;
            animator.Show();
            enabled = true;
        }

        public virtual void Take()
        {
            interactableCollider.enabled = false;
            animator.Enable();
            
            rigidBody.isKinematic = true;

            sway.enabled = true;
            
            enabled = true;
        }

        public virtual void ThrowAway()
        {
            interactableCollider.enabled = true;
            sway.enabled = false;
            animator.Disable();

            rigidBody.isKinematic = false;

            transform.parent = null;
            enabled = false;
        }
        
        protected Vector2 CalculateRecoil()
        {
            return new()
            {
                x = Random.Range(-recoil.maxHorizontalRecoil, recoil.maxHorizontalRecoil),
                y = Random.Range(recoil.minVerticalRecoil, recoil.maxVerticalRecoil)
            };
        }
    }
}