using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Enemy/Create enemy drop settings", fileName = "Enemy drop settings")]
public class DropSettings: ScriptableObject
{
    [SerializeField] private List<DropChance> dropChances;


    public InventoryItemInfo GetItemInfo()
    {
        foreach (var dropChance in dropChances)
        {
            if (CheckChance(dropChance))
                return dropChance.item;
        }
        return null;
    }

    private bool CheckChance(DropChance dropChance) => 
        Random.Range(0, 1f) <= dropChance.chance;

    [Serializable]
    private class DropChance
    {
        public InventoryItemInfo item;
        [Range(.1f, 1f)] public float chance;
    }
}