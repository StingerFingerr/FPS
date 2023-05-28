using DG.Tweening;
using TMPro;
using UnityEngine;
using Weapons;

namespace UI.Game.Game_UI
{
    public class UIAmmo: MonoBehaviour
    {
        public TextMeshProUGUI ammoText;
        public float showDuration = .5f;
        public float hideDuration = .2f;

        private WeaponBase _weapon;
        private bool _isActive;
        
        
        public void SetWeapon(WeaponBase weapon)
        {
            if (weapon is null)
            {
                Hide();
                
                if (_weapon is not null)
                {
                    Unsubscribe();
                }
                _weapon = null;
            }
            else
            {
                _weapon = weapon;
                Subscribe();
                Show();
            }
        }

        private void Subscribe()
        {
            _weapon.OnShot += UpdateAmmo;
            _weapon.OnEndReloading += UpdateAmmo;
            if (_weapon.attachmentSystem)
                _weapon.attachmentSystem.OnModuleChanged += UpdateAmmo;
        }

        private void Unsubscribe()
        {
            _weapon.OnShot -= UpdateAmmo;
            _weapon.OnEndReloading -= UpdateAmmo;
            if (_weapon.attachmentSystem)
                _weapon.attachmentSystem.OnModuleChanged -= UpdateAmmo;
        }

        private void Show()
        {
            UpdateAmmo();
            if (_isActive)
                transform
                    .DOPunchScale(Vector3.one * .5f, showDuration, 0, 1);
            else
                transform
                    .DOScale(1, showDuration)
                    .SetEase(Ease.OutBack);

            _isActive = true;
        }

        private void UpdateAmmo(Vector2 recoil)
        {
            if (_weapon is null)
                return;
            ammoText.text = $"{_weapon.ammoLeft}/{_weapon.MagazineCapacity}";
        }

        private void UpdateAmmo()
        {
            if (_weapon is null)
                return;
            ammoText.text = $"{_weapon.ammoLeft}/{_weapon.MagazineCapacity}";
        }

        public void Hide()
        {
            _weapon = null;
            
            transform
                .DOScale(0, hideDuration)
                .SetEase(Ease.InBack);
            
            _isActive = false;
        }
    }
}