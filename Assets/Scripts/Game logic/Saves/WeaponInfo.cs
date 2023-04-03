using System;
using System.Collections.Generic;

namespace Game_logic
{
    [Serializable]
    public class WeaponInfo
    {
        public string name;
        public bool isHidden;
        public List<InventoryItemInfo> attachmentItems;
    }
}