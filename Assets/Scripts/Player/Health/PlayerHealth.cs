using System;
using Game_logic;
using Infrastructure;
using MoreMountains.Feedbacks;
using Player.Inputs;
using UnityEngine;
using Zenject;

public class PlayerHealth: MonoBehaviour, IDamageable, IProgressReader, IProgressWriter, IProgressInitializer
{
    [SerializeField] private MMF_Player takeDamageFeedback;
    [SerializeField] private int maxHealth;

    [SerializeField] private InventoryItemInfo medKitLittle;
    [SerializeField] private InventoryItemInfo medKitBig;

    private float _medKitLittleHealthMultiplier = .2f;

    public Action<float> onHealthChanged;
    public Action onPlayerDied;

    private PlayerInputs _playerInputs;
    private IInventory _inventory;
    
    private int _currentHealth;

    [Inject]
    private void Construct(IInventory inventory) => 
        _inventory = inventory;

    private void Awake()
    {
        _playerInputs = GetComponent<PlayerInputs>();
        _playerInputs.onHeal += Heal;
    }

    private void Start() => 
        onHealthChanged?.Invoke(GetHealthValue());

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
        InvokeHealthChanged();
    }

    public void Load(Progress progress) => 
        _currentHealth = progress.playerState.health;

    public void Save(Progress progress) => 
        progress.playerState.health = _currentHealth;

    public void InitializeProgressData() => 
        _currentHealth = maxHealth;

    private void Heal()
    {
        var health = GetHealthValue();
        
        if (health >= 1f) 
            return;

        if (health > .5)
        {
            if(TryUseMedKitLittle() is false)
                TryUseMedKitBig();
        }
        else
        {
            if (TryUseMedKitBig() is false)
                TryUseMedKitLittle();
        }
    }

    private bool TryUseMedKitBig()
    {
        if (TryGetMedKit(medKitBig))
        {
            _currentHealth = maxHealth;
            InvokeHealthChanged();
            return true;
        }
        return false;
    }

    private bool TryUseMedKitLittle()
    {
        if (TryGetMedKit(medKitLittle))
        {
            _currentHealth += (int) (maxHealth * _medKitLittleHealthMultiplier);
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            InvokeHealthChanged();
            return true;
        }
        return false;
    }

    private bool TryGetMedKit(InventoryItemInfo medKitInfo) => 
        _inventory.RemoveItemAmount(medKitInfo, 1) >= 1;

    private void InvokeHealthChanged() => 
        onHealthChanged?.Invoke(GetHealthValue());

    private float GetHealthValue() => 
        _currentHealth * 1f / maxHealth;
}