using System;
using System.Collections.Generic;
using Weapon;

namespace Game_logic
{
    [Serializable]
    public class Progress
    {
        public PlayerState PlayerState = new();
        public Dictionary<string, WeaponInfo> WeaponInfos = new();
        public WeaponHolderInfo weaponHolderInfo = new();

    }
}