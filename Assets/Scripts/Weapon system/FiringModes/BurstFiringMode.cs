using System;
using System.Collections;
using UnityEngine;

namespace Weapon.FiringModes
{
    public class BurstFiringMode: FiringModeBase
    {
        public uint burstSize = 2;
        public float timeBetweenShots = .15f;
        public float timeBetweenBursts = .2f;

        private bool _canShot = true;
        private bool _burstTimeoutExpired = true;
        
        
        public override void StartFiring(Action shot)
        {
            if (_canShot && _burstTimeoutExpired)            
                StartCoroutine(StartBurt(shot));
        }

        private IEnumerator StartBurt(Action shot)
        {
            _canShot = false;
            _burstTimeoutExpired = false;
            
            for (int i = 0; i < burstSize; i++)
            {
                shot?.Invoke();
                yield return new WaitForSeconds(timeBetweenShots);
            }

            StartCoroutine(StartBurstTimeout());
        }

        private IEnumerator StartBurstTimeout()
        {
            yield return new WaitForSeconds(timeBetweenBursts);
            _burstTimeoutExpired  = true;
        }

        public override void FinishFiring() => 
            _canShot = true;
    }
}