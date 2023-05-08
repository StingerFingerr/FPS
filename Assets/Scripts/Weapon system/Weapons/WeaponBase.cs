using System;
using System.Collections;
using Animation;
using Attachment_system;
using Shooting;
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
        
        [Header("Shooting")]
        public float spreadingFor10Units = .1f;
        public bool zeroSpreadingIfAiming = true;
        public Transform bulletSpawnPoint;
        public int damage = 100;

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
        public int ammoLeft = 30;
        [SerializeField] private int defaultMagazineCapacity = 30;
        public int MagazineCapacity => OverrideMagazineCapacity();
        public float reloadingTime = 2f;
        public InventoryItemInfo ammoItem;

        [HideInInspector]public bool isHidden;

        public event Action<bool> OnAiming;
        public event Action<Vector2> OnShot;
        public event Action OnStartReloading;
        public event Action OnEndReloading;

        public bool IsAiming { get; private set; }

        private IInventory _inventory;
        protected Bullet.Factory BulletFactory;
        protected bool IsReloading;
        protected bool IsRunning;
        private Camera _mainCamera;

        [Inject]
        private void Construct( IInventory inventory, Bullet.Factory bulletFactory)
        {
            _inventory = inventory;
            BulletFactory = bulletFactory;
        }

        private void OnSprint(InputValue inputValue) => 
            IsRunning = inputValue.isPressed;

        private void OnAim(InputValue inputValue)
        {
            if(isHidden)
                return;
            IsAiming = inputValue.isPressed;
            Aim(IsAiming);
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
            int requiredAmount = MagazineCapacity;

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

        protected void Shot(Vector2 recoil)
        {
            SpawnBullet();
            OnShot?.Invoke(recoil);
        }

        protected virtual void SpawnBullet()
        {
            var damageAmount = damage;
            if (attachmentSystem is not null)
                damageAmount = attachmentSystem.OverrideDamage(damageAmount);

            BulletFactory.Create(bulletSpawnPoint.position, GetDestinationPos(), damageAmount);
        }

        protected Vector3 GetDestinationPos()
        {
            _mainCamera ??= GetComponentInParent<Camera>();
            
            Vector3 trailStart = bulletSpawnPoint.position;
            Vector3 trailEnd;
            
            var ray = _mainCamera.ScreenPointToRay(GetScreenCenter());
            //if (Physics.Raycast(ray, out RaycastHit hit, 100))
            //    trailEnd = hit.point;
            //else
            //    trailEnd = ray.origin.normalized + ray.direction.normalized * 100;
            trailEnd = ray.origin.normalized + ray.direction.normalized * 100;

            return AddSpreading(trailStart, trailEnd);
        }

        protected Vector3 GetScreenCenter()
        {
            var screenPos = new Vector3()
            {
                x = Screen.width / 2,
                y = Screen.height / 2,
                z = _mainCamera.farClipPlane
            };
            return screenPos;
        }

        private Vector3 AddSpreading(Vector3 start, Vector3 end)
        {
            if (IsAiming && zeroSpreadingIfAiming)
                return end;
            
            float dist = Vector3.Distance(start, end);
            float spreading = dist / 10 * spreadingFor10Units;
            Vector3 offset = new Vector3()
            {
                x = Random.Range(-spreading, spreading),
                y = Random.Range(-spreading, spreading),
                z = Random.Range(-spreading, spreading)
            };
            return end + offset;
        }

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

        private int OverrideMagazineCapacity()
        {
            if (attachmentSystem is null)
                return defaultMagazineCapacity;

            return attachmentSystem.OverrideMagazineCapacity(defaultMagazineCapacity);
        }
    }
}