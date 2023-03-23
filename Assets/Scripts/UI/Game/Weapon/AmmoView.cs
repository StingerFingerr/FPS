using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    public class AmmoView: MonoBehaviour
    {
        public TextMeshProUGUI ammoText;
        public float showDuration = .5f;
        public float hideDuration = .2f;

        private bool _isActive;
        
        
        public void Show(int currentAmmo, int maxAvailableAmmo)
        {
            ammoText.text = $"{currentAmmo} / {maxAvailableAmmo}";

            if (_isActive)
                transform
                    .DOPunchScale(Vector3.one * .5f, showDuration, 0, 1);
            else
                transform
                    .DOScale(1, showDuration)
                    .SetEase(Ease.OutBack);
            
            _isActive = true;
        }

        public void Hide()
        {
            
            
            transform
                .DOScale(0, hideDuration)
                .SetEase(Ease.InBack);
            
            _isActive = false;
        }


    }
}