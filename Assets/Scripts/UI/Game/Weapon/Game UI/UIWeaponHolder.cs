using DG.Tweening;
using UnityEngine;
using Weapons;
using Zenject;

namespace UI.Game.Game_UI
{
    public class UIWeaponHolder: MonoBehaviour
    {
        public UIWeaponSlot[] weaponSlotViews;
        public Sprite emptyWeaponSlotIcon;
        public UIAmmo uiAmmo;

        public RectTransform slots;
        public float[] slotsYPos;
        public float moveSlotDuration = 1f;

        public float transparent = .5f;
        public float normal = 1f;
        public float fadingDuration = 1f;

        public float activeScale = 1.5f;
        public float inactiveScale = 1f;
        public float doScaleDuration = 1f;
        
        private  WeaponHolder _weaponHolder;
        
        [Inject]
        private void Construct(WeaponHolder weaponHolder)
        {
            _weaponHolder = weaponHolder;
        }

        private void Start()
        {
            for (int i = 0; i < weaponSlotViews.Length; i++)
            {
                WeaponBase weapon = _weaponHolder.weaponSlots[i].weapon;
                SetWeaponSlotView(weapon, i);
            }
        }

        private void OnEnable() => 
            _weaponHolder.OnWeaponSwitched += SwitchWeapon;

        private void OnDisable() => 
            _weaponHolder.OnWeaponSwitched -= SwitchWeapon;

        private void SwitchWeapon(WeaponBase weapon, int index)
        {
            uiAmmo.SetWeapon(weapon);

            SetWeaponSlotView(weapon, index);
            SetActiveSlotByIndex(index);
        }

        private void SetWeaponSlotView(WeaponBase weapon, int index) => 
            weaponSlotViews[index].SetView(weapon is null ? emptyWeaponSlotIcon : _weaponHolder.weaponSlots[index].weapon.icon);

        private void SetActiveSlotByIndex(int index)
        {
            slots
                .DOLocalMoveY(slotsYPos[index], moveSlotDuration)
                .SetEase(Ease.OutBack);
            
            for (int i = 0; i < weaponSlotViews.Length; i++)
            {
                UIWeaponSlot slot = weaponSlotViews[i];
                if (i == index)
                {
                    DOTween.Sequence()
                        .Append(slot.weaponIcon.DOFade(normal, fadingDuration))
                        .Append(slot.transform
                            .DOScale(activeScale, doScaleDuration)
                            .SetEase(Ease.OutCubic));
                }
                else
                {
                    DOTween.Sequence()
                        .Append(slot.weaponIcon.DOFade(transparent, fadingDuration))
                        .Append(slot.transform
                            .DOScale(inactiveScale, doScaleDuration)
                            .SetEase(Ease.Linear));
                }
            }

        }
    }
}