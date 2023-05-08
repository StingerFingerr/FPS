using UnityEngine;

[CreateAssetMenu(menuName = "Enemy settings", fileName = "zombie settings")]
public class EnemySettings: ScriptableObject
{
    public float patrollingRadius = 5f;
    public float patrollingSpeed = .35f;
    public float chasingSpeed = 2f;

    public float checkDestinationTime = 1f;
    public float stoppingDistance = 2f;

    public float minHealth = 100;
    public float maxHealth = 150;
}