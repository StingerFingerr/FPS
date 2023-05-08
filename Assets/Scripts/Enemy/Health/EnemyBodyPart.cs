using UnityEngine;

public class EnemyBodyPart: MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyHealth health;
    [SerializeField] [Range(0, 3)] private float damageSensitivity = 1f;
    
    public virtual void SetDamage(float damage) => 
        health.SetDamage(damage * damageSensitivity);
}