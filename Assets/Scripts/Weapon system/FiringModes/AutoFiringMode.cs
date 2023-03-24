using System;
using System.Collections;
using UnityEngine;
using Weapon.FiringModes;

namespace FiringModes
{
    public class AutoFiringMode: FiringModeBase
    {
        public float timeBetweenShots = .1f;

        private Coroutine _autoFiring;
        
        public override void StartFiring(Action shot) => 
            _autoFiring = StartCoroutine(StarAutoFiring(shot));

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
            if(_autoFiring is not null)
                StopCoroutine(_autoFiring);
        }
    }
}