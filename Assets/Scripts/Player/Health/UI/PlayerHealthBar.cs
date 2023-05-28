using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealthBar: MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI healthText;

    private PlayerHealth _health;

    [Inject]
    private void Construct(PlayerHealth health) => 
        _health = health;

    private void OnEnable() => 
        _health.onHealthChanged += UpdateHealth;

    private void OnDisable() => 
        _health.onHealthChanged -= UpdateHealth;

    private void UpdateHealth(float normHealth)
    {
        slider
            .DOValue(normHealth, 1f)
            .SetEase(Ease.OutExpo);
        healthText.text = $"{(int) (normHealth * 100)}%";
    }
}