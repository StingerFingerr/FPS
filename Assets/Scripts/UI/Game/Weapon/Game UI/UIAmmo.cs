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
                    _weapon.OnShot -= UpdateAmmo;
                    _weapon.OnEndReloading -= UpdateAmmo;
                }
                _weapon = null;
            }
            else
            {
                _weapon = weapon;
                _weapon.OnShot += UpdateAmmo;
                _weapon.OnEndReloading += UpdateAmmo;
                Show();
            }
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

        private void UpdateAmmo(Vector2 recoil) => 
            ammoText.text = $"{_weapon.ammoLeft}/{_weapon.magazineCapacity}";
        private void UpdateAmmo() => 
            ammoText.text = $"{_weapon.ammoLeft}/{_weapon.magazineCapacity}";

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