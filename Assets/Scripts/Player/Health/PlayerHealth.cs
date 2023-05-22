using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerHealth: MonoBehaviour, IDamageable
{
    [SerializeField] private MMF_Player takeDamageFeedback;
    [SerializeField] private int maxHealth;

    public Action<float> onHealthChanged;
    public Action onPlayerDied;
    
    private int _currentHealth;


    private void Start()
    {
        _currentHealth = maxHealth;
        onHealthChanged?.Invoke(GetHealthValue());
    }

    public void SetDamage(int damage, Vector3 hitNormal)
    {
        if(_currentHealth <= 0)
            return;
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            onPlayerDied?.Invoke();
        }
        
        takeDamageFeedback.PlayFeedbacks();
        onHealthChanged?.Invoke(GetHealthValue());
    }

    private float GetHealthValue() => 
        _currentHealth * 1f / maxHealth;
}