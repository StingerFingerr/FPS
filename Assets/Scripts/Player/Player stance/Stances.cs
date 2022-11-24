using UnityEngine;

namespace Player.Player_stance
{
    public class Stances : MonoBehaviour
    {
        public CharacterController characterController;
        public Transform cameraHolder;
        
        public Stance currentStance;

        public StanceSettings normal;
        public StanceSettings crouch;
        public StanceSettings prone;

        public float changeStanceSpeed = 5f;

        private void Update()
        {
            CalculateStance();
        }

        private void CalculateStance()
        {
            float targetHeight = GetTargetHeight();
            Vector3 targetCenter = GetTargetCenter();
            Vector3 targetCameraPos = GetTargetCameraPos();
            float t = Time.deltaTime * changeStanceSpeed;

            characterController.height = Mathf.Lerp(characterController.height, targetHeight,t);
            characterController.center = Vector3.Lerp(characterController.center, targetCenter, t);
            cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, targetCameraPos, t);
        }

        private float GetTargetHeight()
        {
            return currentStance switch
            {
                Stance.Normal => normal.height,
                Stance.Crouch => crouch.height,
                Stance.Prone => prone.height,
                _ => normal.height
            };
        }

        private Vector3 GetTargetCenter()
        {
            return currentStance switch
            {
                Stance.Normal => normal.center,
                Stance.Crouch => crouch.center,
                Stance.Prone => prone.center,
                _ => normal.center
            };
        }
        private Vector3 GetTargetCameraPos()
        {
            return currentStance switch
            {
                Stance.Normal => normal.cameraPos,
                Stance.Crouch => crouch.cameraPos,
                Stance.Prone => prone.cameraPos,
                _ => normal.cameraPos
            };
        }

        public void OnCrouch()
        {
            if (currentStance is Stance.Crouch)
                currentStance = Stance.Normal;
            else
                currentStance = Stance.Crouch;
        }
        public void OnProne()
        {
            if (currentStance is Stance.Prone)
                currentStance = Stance.Normal;
            else
                currentStance = Stance.Prone;
        }
        
        
        
    }
}
