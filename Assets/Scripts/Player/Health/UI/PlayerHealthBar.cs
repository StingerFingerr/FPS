using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealthBar: MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Image sliderImage;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color poisonedColor;

    private PlayerHealth _health;

    [Inject]
    private void Construct(PlayerHealth health) => 
        _health = health;

    private void OnEnable()
    {
        _health.onHealthChanged += UpdateHealth;
        _health.onPoisoning += HandlePoisoning;
    }

    private void OnDisable()
    {
        _health.onHealthChanged -= UpdateHealth;
        _health.onPoisoning -= HandlePoisoning;
    }

    private void HandlePoisoning(bool isPoisoned)
    {
        if (isPoisoned)
            SetAsPoisoned();
        else
            SetAsNormal();
    }

    private void SetAsPoisoned() => 
        sliderImage.DOColor(poisonedColor, 1f);

    private void SetAsNormal() => 
        sliderImage.DOColor(normalColor, 1f);

    private void UpdateHealth(float normHealth)
    {
        slider
            .DOValue(normHealth, 1f)
            .SetEase(Ease.OutExpo);
        healthText.text = $"{(int) (normHealth * 100)}%";
    }
}