using System;
using System.Collections;
using Game_logic;
using Infrastructure;
using MoreMountains.Feedbacks;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamageable, IProgressReader, IProgressWriter, IProgressInitializer
{
    [SerializeField] private MMF_Player takeDamageFeedback;
    [SerializeField] private int maxHealth;

    [SerializeField] private InventoryItemInfo medKitLittle;
    [SerializeField] private InventoryItemInfo medKitBig;

    [SerializeField] private PoisoningSettings poisoningSettings;
    [SerializeField] private InventoryItemInfo antidote;

    private float _medKitLittleHealthMultiplier = .2f;

    public event Action<float> onHealthChanged;
    public event Action onPlayerDied;
    public event Action<bool> onPoisoning;

    private PlayerInputs _playerInputs;
    private IInventory _inventory;

    private int _currentHealth;
    private bool _isPoisoned;

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
        if (IsDead())
            return;
        _currentHealth -= damage;

        if (IsDead())
        {
            _currentHealth = 0;
            onPlayerDied?.Invoke();
        }

        takeDamageFeedback.PlayFeedbacks();
        InvokeHealthChanged();
    }

    private bool IsDead() => 
        _currentHealth <= 0;

    public void SetPoisoning()
    {
        if (IsDead())
            return;
        if (_isPoisoned)
            return;
        _isPoisoned = true;
        
        StartCoroutine(Poisoning());
        onPoisoning?.Invoke(_isPoisoned);
    }

    private IEnumerator Poisoning()
    {
        while (IsDead() is false && _isPoisoned)
        {
            yield return new WaitForSeconds(poisoningSettings.poisoningDelay);
            SetDamage(poisoningSettings.poisoningDamage, Vector3.zero);
        }
    }

    public void Load(Progress progress) => 
        _currentHealth = progress.playerState.health;

    public void Save(Progress progress) => 
        progress.playerState.health = _currentHealth;

    public void InitializeProgressData() => 
        _currentHealth = maxHealth;

    private void Heal()
    {
        if (_isPoisoned)
        {
            if (TryUseAntidote())
                return;
        }
        
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

    private bool TryUseAntidote()
    {
        if (TryGetMedKit(antidote))
        {
            _isPoisoned = false;
            onPoisoning?.Invoke(_isPoisoned);
            return true;
        }
        return false;
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