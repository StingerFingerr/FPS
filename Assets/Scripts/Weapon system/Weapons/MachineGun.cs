using UnityEngine;
using UnityEngine.InputSystem;
using Weapon.FiringModes;
using Weapon.Recoil;
using Weapon.Sway;

namespace Weapon
{
    public sealed class MachineGun: WeaponBase
    {
        public WeaponAnimator animator;
        public RecoilParameters recoil;
        public BoxCollider interactableCollider;
        public Rigidbody rigidBody;
        public WeaponSway sway;
        public FiringModeBase[] firingModes;
        
        private Vector3 _targetPosition;
        private int _currentFiringMode;

        private void OnEnable() => 
            _targetPosition = hipPosition;

        private void Update()
        {
            UpdateAiming();
        }
        
        private void OnFire(InputValue inputValue)
        {
            if(isHidden)
                return;
            
            if(inputValue.isPressed)
                firingModes[_currentFiringMode].StartFiring(Shot);
            else 
                firingModes[_currentFiringMode].FinishFiring();
        }

        private void OnSwitchFiringMode(InputValue inputValue)
        {
            if(isHidden)
                return;

            _currentFiringMode++;
            if (_currentFiringMode >= firingModes.Length)
                _currentFiringMode = 0;
        }

        private Vector2 CalculateRecoil()
        {
            return new()
            {
                x = Random.Range(-recoil.maxHorizontalRecoil, recoil.maxHorizontalRecoil),
                y = Random.Range(recoil.minVerticalRecoil, recoil.maxVerticalRecoil)
            };
        }

        private void Shot() => 
            base.Shot(CalculateRecoil());


        protected override void Aim(bool aim)
        {
            if (aim)
                StartAiming();
            else
                CancelAiming();
            
            base.Aim(aim);
        }

        private void StartAiming() => 
            _targetPosition = aimPosition;

        private void CancelAiming() => 
            _targetPosition = hipPosition;

        private void UpdateAiming()
        {
            transform.localPosition =
                Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * aimingSpeed);
        }

        public override void Hide()
        {
            isHidden = true;
            animator.Hide();
            enabled = false;
        }

        public override void Show()
        {
            isHidden = false;
            animator.Show();
            enabled = true;
        }

        public override void Interact()
        {
            Take();
        }

        protected override void Take()
        {
            interactableCollider.enabled = false;
            animator.Enable();
            
            rigidBody.isKinematic = true;

            sway.enabled = true;
            
            enabled = true;
        }

        public override void ThrowAway()
        {
            interactableCollider.enabled = true;
            sway.enabled = false;
            animator.Disable();

            rigidBody.isKinematic = false;
            AddForce();


            transform.parent = null;
            enabled = false;
        }

        private void AddForce()
        {
            rigidBody.AddForce(transform.forward * 3 + Vector3.up * 2, ForceMode.VelocityChange);
            rigidBody.AddTorque(-Vector3.up, ForceMode.VelocityChange);
        }
    }
}