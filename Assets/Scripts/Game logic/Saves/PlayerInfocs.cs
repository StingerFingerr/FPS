using System;

namespace Game_logic
{
    [Serializable]
    public class PlayerState
    {
        public Vec3 position;
        public Vec3 rotation;
        public Vec3 cameraRotation;
    }
}