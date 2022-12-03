using System;

namespace Weapon.Sway
{
    [Serializable]
    public struct SwayParameters
    {
        public float swayIntensity;

        public float maxHorizontalSway;
        public float maxVerticalSway;

        public bool invertHorizontalSway;
        public bool invertVerticalSway;
    }
}