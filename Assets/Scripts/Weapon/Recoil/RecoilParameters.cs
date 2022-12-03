using System;

namespace Weapon.Recoil
{
    [Serializable]
    public class RecoilParameters
    {
        public float maxVerticalRecoil = .5f;
        public float minVerticalRecoil = .4f;
        public float maxHorizontalRecoil = .1f;
    }
}