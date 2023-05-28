using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Animation
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
        private static readonly int Hide1 = Animator.StringToHash("Hide");
        private static readonly int Show1 = Animator.StringToHash("Show");

        private void OnEnable()
        {
            weaponBase.OnShot += Shot;
            weaponBase.OnStartReloading += StartReloading;
            weaponBase.OnAiming += SwitchMovingAnimator;
        }

        private void OnDisable()
        {
            weaponBase.OnShot -= Shot;
            weaponBase.OnStartReloading -= StartReloading;
            weaponBase.OnAiming -= SwitchMovingAnimator;
        }

        private void Update()
        {
            if (_isAiming)            
                ResetPosAndRot();
        }

        public void Hide()
        {
            SaveParameters();
            enabled = false;
            movingAnimator.SetTrigger(Hide1);
        }

        public void Show()
        {
            ReadParameters();
            enabled = true;
            movingAnimator.enabled = true;
            movingAnimator.SetTrigger(Show1);
        }

        public void Enable()
        {
            movingAnimator.enabled = true;
            enabled = true;
        }
        public void Disable()
        {
            movingAnimator.enabled = false;
            enabled = false;
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
            if (walking is false)
                movingAnimator.SetBool(Sprint, false);
            movingAnimator.SetBool(Walk, walking);
            _walkParameter = walking;
        }

        private void OnSprint(InputValue inputValue)
        {
            if(weaponBase.allowRun is false)
                return;

            bool sprinting = inputValue.isPressed;
            movingAnimator.SetBool(Sprint, sprinting);
            _sprintParameter = sprinting;
        }

        private void StartReloading()
        {
            if(weaponAnimator)
                weaponAnimator.SetTrigger(Reload);
        }

        private void Shot(Vector2 recoil)
        {
            if(weaponAnimator)
                weaponAnimator.SetTrigger(Fire);
        }

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

            ReadParameters();
        }

        private void TurnOffMovingAnimator()
        {
            movingAnimator.enabled = false;

            SaveParameters();
        }

        private void ReadParameters()
        {
            movingAnimator.SetBool(Walk, _walkParameter);
            movingAnimator.SetBool(Sprint, _sprintParameter);
        }

        private void SaveParameters()
        {
            _walkParameter = movingAnimator.GetBool(Walk);
            _sprintParameter = movingAnimator.GetBool(Sprint);
        }
    }
}