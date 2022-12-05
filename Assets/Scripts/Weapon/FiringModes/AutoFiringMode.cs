using System;
using System.Collections;
using UnityEngine;

namespace Weapon.FiringModes
{
    public class AutoFiringMode: FiringModeBase
    {
        public float timeBetweenShots = .1f;

        private Coroutine _autoFiring;
        
        public override void StartFiring(Action shot)
        {
            _autoFiring = StartCoroutine(StarAutoFiring(shot));
        }

        private IEnumerator StarAutoFiring(Action shot)
        {
            while (true)
            {
                shot?.Invoke();
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }

        public override void FinishFiring()
        {
            StopCoroutine(_autoFiring);
        }
    }
}