using UnityEngine;
using UnityEngine.InputSystem;
using Weapon.FiringModes;

namespace Weapons
{
    public sealed class MachineGun: WeaponBase
    {
        public FiringModeBase[] firingModes;
        
        private Vector3 _targetPosition;
        private int _currentFiringMode;

        private void Start() => 
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

        public override void ThrowAway()
        {
            firingModes[_currentFiringMode].FinishFiring();
            base.ThrowAway();
            AddForce();
        }

        private void AddForce()
        {
            rigidBody.AddForce(transform.forward * 3 + Vector3.up * 2, ForceMode.VelocityChange);
            rigidBody.AddTorque(-Vector3.up, ForceMode.VelocityChange);
        }
    }
}