using System.Collections;
using FiringModes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    [RequireComponent(typeof(AutoFiringMode))]
    public sealed class Minigun: WeaponBase
    {
        public float maxBarrelAngularSpeed;
        public float accelerationTime;
        public float stopTime;

        public Transform barrelTransform;
        public AutoFiringMode autoFiringMode;
        
        private Coroutine _shootingCoroutine;
        private float _currentAngularSpeed = 0;
        private bool _isFiring;

        public AudioSource audioSource;
        public AudioClip reloadClip;
        public AudioClip launchAndFiringClip;
        public AudioClip stoppingClip;
        public AudioClip equippedClip;
        
        private void Update()
        {
            transform.localPosition =
                Vector3.Lerp(transform.localPosition, hipPosition, Time.deltaTime * aimingSpeed);
        }

        private void OnFire(InputValue inputValue)
        {
            if(isHidden)
                return;
            
            if (inputValue.isPressed)
                StartShooting();
            else
                CancelShooting();
        }

        private void StartShooting()
        {
            if(_isFiring)
                return;

            if (CanShooting())            
                _shootingCoroutine = StartCoroutine(LaunchMinigun());
        }

        private IEnumerator LaunchMinigun()
        {
            _isFiring = true;
            
            audioSource.PlayOneShot(launchAndFiringClip);
            
            float timer = 0;

            while (timer < accelerationTime)
            {
                _currentAngularSpeed = Mathf.Lerp(0, maxBarrelAngularSpeed, timer / accelerationTime);

                ApplyBarrelRotation();

                timer += Time.deltaTime;
                yield return null;
            }
            
            autoFiringMode.StartFiring(Shot);

            while (CanShooting())
            {
                ApplyBarrelRotation();
                yield return null;
            }
            
            CancelShooting();
        }

        private void CancelShooting()
        {
            if(_isFiring is false)
                return;

            audioSource.Stop();
            audioSource.PlayOneShot(stoppingClip);

            _isFiring = false;
            autoFiringMode.FinishFiring();
            StopCoroutine(_shootingCoroutine);
            StartCoroutine(StopMinigun());
        }

        private IEnumerator StopMinigun()
        {
            float timer = 0;
            float initialSpeed = _currentAngularSpeed;
            
            while (timer < stopTime)
            {
                _currentAngularSpeed = Mathf.Lerp(initialSpeed, 0, timer / stopTime);
                ApplyBarrelRotation();
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private void ApplyBarrelRotation() => 
            barrelTransform.Rotate(Vector3.forward * _currentAngularSpeed);

        private bool CanShooting() => 
            true;

        private void Shot()
        {
            base.Shot(CalculateRecoil());
        }

        public override void Show()
        {
            audioSource.PlayOneShot(equippedClip);
            base.Show();
        }
        protected override void Reload()
        {
            audioSource.PlayOneShot(reloadClip);
            base.Reload();
        }
        
        public override void ThrowAway()
        {
            StopAllCoroutines();
            StartCoroutine(StopMinigun());
            autoFiringMode.FinishFiring();
            audioSource.Stop();
            
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