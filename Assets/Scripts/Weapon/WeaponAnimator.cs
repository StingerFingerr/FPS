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

        private bool _walkParameter;
        private bool _sprintParameter;
        
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
                ResetPosAndRot();
        }

        private void ResetPosAndRot()
        {
            float deltaTime = Time.deltaTime * weaponBase.aimingSpeed;
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.zero), deltaTime);
        }

        private void OnMove(InputValue inputValue)
        {
            bool walking = inputValue.Get<Vector2>() != Vector2.zero;
            movingAnimator.SetBool(Walk, walking);
            _walkParameter = walking;
        }

        private void OnSprint(InputValue inputValue)
        {
            bool sprinting = inputValue.isPressed;
            movingAnimator.SetBool(Sprint, sprinting);
            _sprintParameter = sprinting;
        }

        private void Reloading() => 
            weaponAnimator.SetTrigger(Reload);
        private void Shot(Vector2 recoil) => 
            weaponAnimator.SetTrigger(Fire);

        private void SwitchMovingAnimator(bool aim)
        {
            _isAiming = aim;
            if (_isAiming)
                TurnOffMovingAnimator();
            else
                TurnOnMovingAnimator();
        }

        private void TurnOnMovingAnimator()
        {
            movingAnimator.enabled = true;
            movingAnimator.Rebind();

            movingAnimator.SetBool(Walk, _walkParameter);
            movingAnimator.SetBool(Sprint, _sprintParameter);
        }

        private void TurnOffMovingAnimator()
        {
            movingAnimator.enabled = false;

            _walkParameter = movingAnimator.GetBool(Walk);
            _sprintParameter = movingAnimator.GetBool(Sprint);
        }
    }
}