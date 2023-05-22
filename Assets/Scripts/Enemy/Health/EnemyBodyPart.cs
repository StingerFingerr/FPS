using UnityEngine;
using Zenject;

public class EnemyBodyPart: MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyHealth health;
    [SerializeField] [Range(0, 3)] private float damageSensitivity = 1f;

    private DamageIndicator.Factory _damageIndicators;

    [Inject]
    private void Construct(DamageIndicator.Factory damageIndicators) => 
        _damageIndicators = damageIndicators;


    public virtual void SetDamage(int damage, Vector3 hitNormal)
    {
        int newDamage = (int) (damage * damageSensitivity);
        health.SetDamage(newDamage);
        _damageIndicators.Create(health.damageIndicatorSpawnPos.position, hitNormal, newDamage);
    }
}