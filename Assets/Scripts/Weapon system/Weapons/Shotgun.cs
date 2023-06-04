using Shooting.Firing_modes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class Shotgun: WeaponBase
    {
        public int minFractions = 5;
        public int maxFractions = 8;
        
        public SingleFiringMode firingMode;
        
        public AudioSource audioSource;
        public AudioClip shotClip;
        public AudioClip preReloadClip;
        public AudioClip reloadClip;
        public AudioClip equippedClip;
        public AudioClip bulletCaseFallingClip;
        
        private Vector3 _targetPosition;
        private int _currentFiringMode;

        private void Start() => 
            _targetPosition = hipPosition;

        private void Update() => 
            UpdateAiming();

        private void OnFire(InputValue inputValue)
        {
            if(isHidden)
                return;
            if(IsReloading)
                return;
            
            if(inputValue.isPressed)
            {
                if (ammoLeft <= 0)
                {
                    Reload();
                    return;
                }
                firingMode.StartFiring(Shot);
            }
            else
            {
                firingMode.FinishFiring();
            }
        }

        private void Shot()
        {
            if (IsRunning)
                return;
            
            audioSource.PlayOneShot(shotClip);
            audioSource.PlayOneShot(bulletCaseFallingClip);
            Invoke(nameof(PlayPreReloadClip), .1f);
            
            ammoLeft--;
            if (ammoLeft <= 0)
            {
                ammoLeft = 0;
                firingMode.FinishFiring();
            }

            base.Shot(CalculateRecoil());
        }

        private void PlayPreReloadClip() => 
            audioSource.PlayOneShot(preReloadClip);

        protected override void SpawnBullet()
        {
            var damageAmount = damage;
            if (attachmentSystem is not null)
                damageAmount = attachmentSystem.OverrideDamage(damageAmount);

            int fractionsAmount = Random.Range(minFractions, maxFractions + 1);
            for (int i = 0; i < fractionsAmount; i++)
                BulletFactory.Create(bulletSpawnPoint.position, GetDestinationPos(), damageAmount);
        }

        protected override void Reload()
        {
            if (IsReloading)
                return;
            
            base.Reload();
            if(IsReloading)
                audioSource.PlayOneShot(reloadClip);
        }

        public override void Show()
        {
            audioSource.PlayOneShot(equippedClip);
            base.Show();
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

        public override void ThrowAway()
        {
            firingMode.FinishFiring();
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