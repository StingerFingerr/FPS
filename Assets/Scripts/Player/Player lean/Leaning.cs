using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Player_lean
{
    public class Leaning : MonoBehaviour
    {
        public Transform leanTransform;
        
        public Lean leanRight;
        public Lean leanLeft;

        public float changeLeanSpeed = 5f;
        
        private Lean _currentLean;
        private Lean _targetLean;
        
        private void Update()
        {
            _currentLean = Lean.Lerp(_currentLean, _targetLean, Time.deltaTime * changeLeanSpeed);
            ApplyLean();
        }

        public void OnLean(InputValue inputValue)
        {
            _targetLean = inputValue.Get<float>() switch
            {
                -1 => leanLeft,
                1 => leanRight,
                _ => Lean.ZeroLean
            };
        }

        private void ApplyLean()
        {
            leanTransform.localPosition = _currentLean.cameraOffset;
            leanTransform.localRotation = Quaternion.Euler(_currentLean.cameraRotation);
        }
    }
}
