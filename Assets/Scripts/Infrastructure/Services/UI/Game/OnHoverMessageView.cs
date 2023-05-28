using DG.Tweening;
using Player.Interaction;
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
        public float hideFadeOffset = 1f;

        private bool _isActive;
        private Tween _hidingTween;
        
        private void OnEnable()
        {
            HoverableObject.onHoverBegin += Show;
            HoverableObject.onHoverEnd += Hide;
        }

        private void OnDisable()
        {
            HoverableObject.onHoverBegin -= Show;
            HoverableObject.onHoverEnd -= Hide;
        }

        private void Show(string message)
        {
            messageText.text = message;

            _hidingTween?.Kill();
            _hidingTween = null;
            
            if (_isActive is false)
            {
                DOTween.Sequence()
                    .Append(messageText.DOFade(1, showFadeDuration))
                    .Append(messageText.transform
                        .DOScale(1, showScaleDuration)
                        .SetEase(Ease.OutQuart));
            }
            _isActive = true;
        }

        private void Hide()
        {
            if (_isActive)
            {
                _hidingTween = DOTween.Sequence()
                    .AppendInterval(hideFadeOffset)
                    .Append(messageText.DOFade(0, hideFadeDuration))
                    .AppendInterval(hideFadeDuration);
            }
            _isActive = false;
        }
    }
}