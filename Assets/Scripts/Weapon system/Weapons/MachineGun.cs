using Shooting;
using Shooting.Firing_modes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public sealed class MachineGun: WeaponBase
    {
        public FiringModeBase[] firingModes;

        public AudioSource audioSource;
        public AudioClip defaultShotClip;
        public AudioClip bulletCaseFallingClip;
        public AudioClip reloadClip;
        public AudioClip equippedClip;
        public AudioClip switchFiringModeClip;
        
        private Vector3 _targetPosition;
        private int _currentFiringMode;

        private bool _playBulletCaseClip;

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
                firingModes[_currentFiringMode].StartFiring(Shot);
            }
            else
            {
                firingModes[_currentFiringMode].FinishFiring();
            }
        }

        private void OnSwitchFiringMode(InputValue inputValue)
        {
            if(isHidden)
                return;

            _currentFiringMode++;
            if (_currentFiringMode >= firingModes.Length)
                _currentFiringMode = 0;
            
            audioSource.PlayOneShot(switchFiringModeClip);
        }

        private void Shot()
        {
            if (IsRunning)
                return;
            
            PlayShotClip();
            
            if(_playBulletCaseClip)
                Invoke(nameof(PlayBulletCaseClip), .4f);
            _playBulletCaseClip = !_playBulletCaseClip;
            
            ammoLeft--;
            if (ammoLeft <= 0)            
                firingModes[_currentFiringMode].FinishFiring();

            base.Shot(CalculateRecoil());
        }

        private void PlayShotClip()
        {
            AudioClip clip = defaultShotClip;
            if (attachmentSystem is not null)
                clip = attachmentSystem.OverrideShotSound(clip);
            audioSource.PlayOneShot(clip);
        }


        private void PlayBulletCaseClip() => 
            audioSource.PlayOneShot(bulletCaseFallingClip);

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