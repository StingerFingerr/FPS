using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Create enemy settings", fileName = "zombie settings")]
public class EnemySettings: ScriptableObject
{
    [Header("Movement")]
    public float patrollingRadius = 5f;
    public float patrollingSpeed = .35f;
    public float chasingSpeed = 2f;

    public float checkDestinationTime = 1f;
    public float stoppingDistance = 2f;

    [Header("Health")]
    public float minHealth = 100;
    public float maxHealth = 150;

    [Header("Damage")]
    public float attackDistance = 2f;
    public int damage = 10;
    [Range(10, 100)] public int setPoisoningChance = 30;
}