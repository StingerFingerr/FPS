using System;

namespace Weapon.Sway
{
    [Serializable]
    public class SwayParameters
    {
        public float swayIntensity = 3f;

        public float maxHorizontalSway = 20f;
        public float maxVerticalSway = 20f;

        public bool invertHorizontalSway;
        public bool invertVerticalSway;
    }
}