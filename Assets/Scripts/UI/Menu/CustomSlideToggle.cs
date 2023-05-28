using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CustomSlideToggle: MonoBehaviour
{
    [SerializeField] private RectTransform handle;
    [SerializeField] private Image handleImage;
    
    [SerializeField] private Vector2 toggleOffPos;
    [SerializeField] private Vector2 toggleOnfPos;

    [SerializeField] private float toggleOffAlfa = .5f;
    [SerializeField] private float toggleOnAlfa = 1f;
    
    [SerializeField] private float toggleDuration = .5f;

    private void Awake()
    {
        var toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(SwitchToggle);
        SwitchToggle(toggle.isOn);
    }

    private void SwitchToggle(bool isOn)
    {
        if (isOn)
        {
            handle.DOAnchorPos(toggleOnfPos, toggleDuration).SetEase(Ease.InOutCubic).SetUpdate(true);
            handleImage.DOFade(toggleOnAlfa, toggleDuration).SetUpdate(true);
        }
        else
        {
            handle.DOAnchorPos(toggleOffPos, toggleDuration).SetEase(Ease.InOutCubic).SetUpdate(true);
            handleImage.DOFade(toggleOffAlfa, toggleDuration).SetUpdate(true);
        }
    }
}