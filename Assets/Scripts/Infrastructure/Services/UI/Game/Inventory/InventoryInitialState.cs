using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItems/Create inventory initial state", fileName = "Inventory initial state")]
public class InventoryInitialState: ScriptableObject
{
    [Serializable]
    public class InventoryInitialItem
    {
        public InventoryItemInfo info;
        public int amount;
    }

    public List<InventoryInitialItem> items;
}