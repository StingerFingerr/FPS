using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Create enemy settings", fileName = "zombie settings")]
public class EnemySettings: ScriptableObject
{
    public float patrollingRadius = 5f;
    public float patrollingSpeed = .35f;
    public float chasingSpeed = 2f;

    public float checkDestinationTime = 1f;
    public float stoppingDistance = 2f;

    public float minHealth = 100;
    public float maxHealth = 150;

    public float attackDistance = 2f;
    public int damage = 10;
}