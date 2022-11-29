using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon
{
    public class WeaponAnimator: MonoBehaviour
    {
        public WeaponBase weaponBase;
        
        public Animator movingAnimator;
        public Animator weaponAnimator;
        
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Sprint = Animator.StringToHash("Sprint");
        private static readonly int Reload = Animator.StringToHash("Reload");
        private static readonly int Fire = Animator.StringToHash("Fire");

        private bool _isAiming;
        
        private void OnEnable()
        {
            weaponBase.OnShot += Shot;
            weaponBase.OnReloading += Reloading;
            weaponBase.OnAiming += SwitchMovingAnimator;
        }

        private void OnDisable()
        {
            weaponBase.OnShot -= Shot;
            weaponBase.OnReloading -= Reloading;
            weaponBase.OnAiming -= SwitchMovingAnimator;
        }

        private void Update()
        {
            if (_isAiming)
            {
                float deltaTime = Time.deltaTime * weaponBase.aimingSpeed;
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.zero), deltaTime);
            }
        }

        private void OnMove(InputValue inputValue) => 
            movingAnimator.SetBool(Walk, inputValue.Get<Vector2>()!=Vector2.zero);

        private void OnSprint(InputValue inputValue) => 
            movingAnimator.SetBool(Sprint, inputValue.isPressed);

        private void Reloading() => 
            weaponAnimator.SetTrigger(Reload);
        private void Shot() => 
            weaponAnimator.SetTrigger(Fire);

        private void SwitchMovingAnimator(bool aim)
        {
            _isAiming = aim;
            if(_isAiming)
                movingAnimator.enabled = false;
            else
                movingAnimator.enabled = true;
        }
    }
}