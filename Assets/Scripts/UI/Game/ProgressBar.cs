using System;
using DG.Tweening;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar: MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI progressText;

    [Header("Feedbacks")] 
    [SerializeField] private MMF_Player showFeedback;
    [SerializeField] private MMF_Player hideFeedback;

    private bool _isOpen;
    
    public void Show(bool needToReset = false)
    {
        if(needToReset)
            ResetProgress();

        if (_isOpen)
            return;
        _isOpen = true;
        showFeedback.PlayFeedbacks();
    }

    public void Hide()
    {
        if (_isOpen is false)
            return;
        _isOpen = false;
        hideFeedback.PlayFeedbacks();
    }

    public void SetNewValue(float value)
    {
        slider
            .DOValue(value, 3)
            .SetEase(Ease.OutExpo);
        progressText.text = $"{(int) (value * 100)}%";
    }

    public void ResetProgress() => 
        SetNewValue(0);
}