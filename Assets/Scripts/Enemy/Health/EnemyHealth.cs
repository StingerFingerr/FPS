using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth: MonoBehaviour
{
    [SerializeField] private EnemySettings settings;
    [SerializeField] private Animator animator;
    [SerializeField] private BaseZombie zombie;
    [SerializeField] public Transform damageIndicatorSpawnPos;

    private float _currentHealth;
    private static readonly int Death = Animator.StringToHash("Death");

    
    private void Start() => 
        Reset();

    public void Reset() => 
        _currentHealth = Random.Range(settings.minHealth, settings.maxHealth);

    public void SetDamage(float damage)
    {
        if(_currentHealth <= 0)
            return;
        
        _currentHealth -= damage;
        
        if(_currentHealth <= 0)
        {
            animator.SetTrigger(Death);
            zombie.Kill();
        }
    }
        
}