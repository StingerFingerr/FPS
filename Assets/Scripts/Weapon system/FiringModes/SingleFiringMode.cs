using System;
using System.Collections;
using UnityEngine;

namespace Weapon.FiringModes
{
    public class SingleFiringMode: FiringModeBase
    {
        public float timeBetweenShots = .3f;

        private bool _canShot = true;
        private bool _shotTimeoutExpired = true;
        public override void StartFiring(Action shot)
        {
            if(_canShot && _shotTimeoutExpired)
            {
                StartCoroutine(StartShotTimeout());
                shot?.Invoke();
            }
            
            _canShot = false;
            _shotTimeoutExpired = false;
        }

        private IEnumerator StartShotTimeout()
        {
            yield return new WaitForSeconds(timeBetweenShots);
            _shotTimeoutExpired = true;
        }

        public override void FinishFiring() => 
            _canShot = true;
    }
}