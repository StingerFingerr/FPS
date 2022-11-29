using System;

namespace Player.Player_settings
{
    [Serializable]
    public class SpeedSettings
    {
        public float moveSpeed = 4.0f;
        public float sprintSpeed = 6.0f;
        public float rotationSpeed = 1.0f;

        public float crouchSpeedModifier = .6f;
        public float proneSpeedModifier = .2f;

        public float speedChangeRate = 10.0f;
    }
}