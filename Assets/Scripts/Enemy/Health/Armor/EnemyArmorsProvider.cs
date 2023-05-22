using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy armor/Create armor provider", fileName = "Enemy armors provider")]
public class EnemyArmorsProvider: ScriptableObject
{
    [SerializeField] private List<EnemyArmorData> armors;

    public EnemyArmorData GetData(EnemyArmorType type)
    {
        var allArmors = armors.FindAll(a => a.type == type);
        return allArmors[Random.Range(0, allArmors.Count)];
    }
}