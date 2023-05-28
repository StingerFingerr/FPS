using System;
using System.Collections.Generic;

namespace Game_logic
{
    [Serializable]
    public class Progress
    {
        public PlayerState playerState = new();
        public Dictionary<string, WeaponInfo> WeaponInfos = new();
        public WeaponHolderInfo weaponHolderInfo = new();
        public List<InventorySlot> inventorySlots = new();
        public Dictionary<string, EnemySpawnerInfo> EnemySpawnerInfos = new();
    }
}