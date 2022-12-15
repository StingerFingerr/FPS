using System;

namespace Game_logic
{
    [Serializable]
    public class Vec3
    {
        public float x;
        public float y;
        public float z;

        public Vec3 ()
        {
            
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}