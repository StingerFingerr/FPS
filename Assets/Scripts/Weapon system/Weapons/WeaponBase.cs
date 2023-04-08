using System;
using System.Collections;
using Animation;
using Attachment_system;
using UI.Game.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon.Recoil;
using Weapon.Sway;
using Zenject;
using Random = UnityEngine.Random;

namespace Weapons
{
    public abstract class WeaponBase: MonoBehaviour
    {
        public class Factory: PlaceholderFactory<string, WeaponBase>
        { }

        public new string name;
        public CrosshairType crosshairType;

        [Header("Aiming")]
        public Vector3 hipPosition;
        public Vector3 aimPosition;
        public float aimingSpeed = 10f;

        [Header("Other")]
        public WeaponAnimator animator;
        public RecoilParameters recoil;
        public BoxCollider interactableCollider;
        public WeaponSway sway;
        public Rigidbody rigidBody;
        public AttachmentSystem attachmentSystem;

        [Header("UI")]
        public Sprite icon;
        public UIInventoryWeaponItem uiInventoryWeaponItem;

        public bool allowRun = true;

        [Header("Magazine")] 
        public int ammoLeft = 0;
        public int magazineCapacity = 30;
        public float reloadingTime = 2f;
        public InventoryItemInfo ammoItem;
        
        [HideInInspector]public bool isHidden;
        
        public event Action<bool> OnAiming;
        public event Action<Vector2> OnShot;
        public event Action OnStartReloading;
        public event Action OnEndReloading;

        private IInventory _inventory;
        protected bool IsReloading;

        [Inject]
        private void Construct(IInventory inventory) => 
            _inventory = inventory;

        private void OnAim(InputValue inputValue)
        {
            if(isHidden)
                return;
            
            Aim(inputValue.isPressed);
        }

        private void OnReload(InputValue inputValue)
        {
            if(isHidden)
                return;
            if (IsReloading)
                return;
            Reload();
        }

        protected virtual void Reload()
        {
            int requiredAmount = magazineCapacity;
            if (attachmentSystem is not null)
                requiredAmount = attachmentSystem.OverrideMagazineCapacity(magazineCapacity);

            if(requiredAmount == ammoLeft)
                return;
            requiredAmount -= ammoLeft;
            
            int availableAmount = _inventory.RemoveItemAmount(ammoItem, requiredAmount);

            if (availableAmount > 0)
            {
                StartCoroutine(Reloading(availableAmount + ammoLeft));
                IsReloading = true;
                OnStartReloading?.Invoke();
            }
        }

        private IEnumerator Reloading(int ammo)
        {
            yield return new WaitForSeconds(reloadingTime);
            ammoLeft = ammo;
            IsReloading = false;
            OnEndReloading?.Invoke();
        }

        protected void Shot(Vector2 recoil) => 
            OnShot?.Invoke(recoil);
        protected virtual void Aim(bool aim) => 
            OnAiming?.Invoke(aim);
        
        public virtual void Hide()
        {
            isHidden = true;
            animator.Hide();
            enabled = false;
        }

        public virtual void Show()
        {
            isHidden = false;
            animator.Show();
            enabled = true;
        }

        public virtual void Take()
        {
            interactableCollider.enabled = false;
            animator.Enable();
            
            rigidBody.isKinematic = true;

            sway.enabled = true;
            
            enabled = true;
        }

        public virtual void ThrowAway()
        {
            interactableCollider.enabled = true;
            sway.enabled = false;
            animator.Disable();

            rigidBody.isKinematic = false;

            transform.parent = null;
            enabled = false;
        }
        
        protected Vector2 CalculateRecoil()
        {
            return new()
            {
                x = Random.Range(-recoil.maxHorizontalRecoil, recoil.maxHorizontalRecoil),
                y = Random.Range(recoil.minVerticalRecoil, recoil.maxVerticalRecoil)
            };
        }
    }
}