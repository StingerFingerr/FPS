using System;
using System.Collections.Generic;

namespace Game_logic
{
    [Serializable]
    public class Progress
    {
        public PlayerState PlayerState = new();
        public Dictionary<string, WeaponInfo> WeaponInfos = new();
    }
}