using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class OnHoverMessageView: MonoBehaviour
    {
        public TextMeshProUGUI messageText;
        public float showFadeDuration = .5f;
        public float showScaleDuration = .5f;

        public float hideFadeDuration = .5f;

        public void Show(string message)
        {
            messageText.text = message;

            DOTween.Sequence()
                .Append(messageText.DOFade(1, showFadeDuration))
                .Append(messageText.transform
                    .DOScale(1, showScaleDuration)
                    .SetEase(Ease.OutQuart));

        }

        public void Hide()
        {
            DOTween.Sequence()
                .Append(messageText.DOFade(0, hideFadeDuration))
                .AppendInterval(hideFadeDuration)
                .onComplete += () => messageText.transform.localScale = Vector3.zero;
        }
        
    }
}