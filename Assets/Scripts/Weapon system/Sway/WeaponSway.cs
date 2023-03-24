using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapon.Sway
{
    public class WeaponSway: MonoBehaviour
    {
        public SwayParameters normalLookSway;
        public SwayParameters aimingLookSway;
        public SwayParameters normalMoveSway;
        public SwayParameters aimingMoveSway;
        
        public float swaySpeed = 8f;
        
        private SwayParameters _currentLookSwayParameters;
        private SwayParameters _currentMoveSwayParameters;

        private Quaternion _targetLookSway = quaternion.Euler(Vector3.zero);
        private Quaternion _targetMoveSway = quaternion.Euler(Vector3.zero);


        private void Start() => 
            SetNormalSwayParameters();

        private void Update() => 
            UpdateSway();

        private void UpdateSway() => 
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetLookSway * _targetMoveSway, Time.deltaTime * swaySpeed);

        private void OnLook(InputValue inputValue) => 
            CalculateLookSway(inputValue.Get<Vector2>());
        private void OnMove(InputValue inputValue) => 
            CalculateMoveSway(inputValue.Get<Vector2>());

        private void OnAim(InputValue inputValue)
        {
            if (inputValue.isPressed)
                SetAimingSwayParameters();
            else
                SetNormalSwayParameters();
        }

        private void CalculateMoveSway(Vector2 move) => 
            _targetMoveSway = CalculateSway(move, _currentMoveSwayParameters);

        private void CalculateLookSway(Vector2 look) => 
            _targetLookSway = CalculateSway(look, _currentLookSwayParameters);

        public Quaternion CalculateSway(Vector2 input, SwayParameters parameters)
        {
            float offsetX = input.x * parameters.swayIntensity;
            float offsetY = input.y * parameters.swayIntensity;

            offsetX = Mathf.Clamp(offsetX, -parameters.maxHorizontalSway, parameters.maxHorizontalSway);
            offsetY = Mathf.Clamp(offsetY, -parameters.maxVerticalSway, parameters.maxVerticalSway);

            offsetX *= parameters.invertHorizontalSway ? 1 : -1;
            offsetY *= parameters.invertVerticalSway ? 1 : -1;
            
            Quaternion rotationX = Quaternion.AngleAxis(offsetY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(offsetX, Vector3.up);

            return rotationX * rotationY;
        }

        private void SetNormalSwayParameters()
        {
            _currentLookSwayParameters = normalLookSway;
            _currentMoveSwayParameters = normalMoveSway;
        }

        private void SetAimingSwayParameters()
        {
            _currentLookSwayParameters = aimingLookSway;
            _currentMoveSwayParameters = aimingMoveSway;
        }
    }
}