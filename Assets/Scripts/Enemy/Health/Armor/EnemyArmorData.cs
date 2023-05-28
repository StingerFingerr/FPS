using UnityEngine;

[CreateAssetMenu(menuName = "Enemy armor/Create armor data", fileName = "Enemy armor data")]
public class EnemyArmorData: ScriptableObject
{
    public EnemyArmorType type;
    public GameObject armorPrefab;
    [Range(0f,1f)] public float damageMultiplier = .5f;

}