using UnityEngine;
using Weapon.Recoil;

namespace Weapon
{
    public sealed class MachineGun: WeaponBase
    {
        public RecoilParameters recoil;
        
        private Vector3 _targetPosition;

        private void OnEnable() => 
            _targetPosition = hipPosition;

        private void Update()
        {
            UpdateAiming();
        }

        protected override void Shot(Vector2 recoil)
        {
            recoil = CalculateRecoil();
            base.Shot(recoil);
        }

        private Vector2 CalculateRecoil()
        {
            return new()
            {
                x = Random.Range(-recoil.maxHorizontalRecoil, recoil.maxHorizontalRecoil),
                y = Random.Range(recoil.minVerticalRecoil, recoil.maxVerticalRecoil)
            };
        }
        

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
    }
}