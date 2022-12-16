using Player;
using UnityEngine;
using Weapon;
using Zenject;

public class CrosshairSetuper: MonoBehaviour
{
    private WeaponHolder _weaponHolder;
    private WeaponBase _weapon;
    private FirstPersonController _player;
    private DynamicCrosshairBase.CachedFactory _crosshairCachedFactory;

    private DynamicCrosshairBase _crosshair;
    
    [Inject]
    private void Construct(
        WeaponHolder weaponHolder, 
        FirstPersonController player,
        DynamicCrosshairBase.CachedFactory crosshairCachedFactory)
    {
        _weaponHolder = weaponHolder;
        _player = player;
        _crosshairCachedFactory = crosshairCachedFactory;
    }

    private void OnEnable() => 
        _weaponHolder.SwitchCurrentWeapon += SetNewCrosshairFor;

    private void OnDisable() => 
        _weaponHolder.SwitchCurrentWeapon -= SetNewCrosshairFor;

    private void Update()
    {
        if (_crosshair is not null)
        {
            _crosshair.OnMove(_player.IsMoving);
            _crosshair.OnLook(_player.IsLooking);
        }
    }

    private void SetNewCrosshairFor(WeaponBase weapon)
    {
        SwitchWeapon(weapon);
        CreateNewCrossHair(weapon);
    }

    private void CreateNewCrossHair(WeaponBase weapon)
    {
        _crosshair?.Deactivate();

        if (weapon)
            _crosshair = _crosshairCachedFactory.Create(weapon.crosshairType);
        else
            _crosshair = _crosshairCachedFactory.Create(CrosshairType.None);

        _crosshair?.transform.SetParent(transform);
        
        _crosshair.Reset();
        _crosshair.Activate();
    }

    private void SwitchWeapon(WeaponBase weapon)
    {
        if (_weapon)
            Unsubscribe();
        _weapon = weapon;
        if (_weapon)
            Subscribe();
    }

    private void OnAim(bool isAiming) => 
        _crosshair.OnAim(isAiming);

    private void OnShot(Vector2 recoil) => 
        _crosshair.OnShot();

    private void Subscribe()
    {
        _weapon.OnShot += OnShot;
        _weapon.OnAiming += OnAim;
    }

    private void Unsubscribe()
    {
        _weapon.OnShot -= OnShot;
        _weapon.OnAiming -= OnAim;
    }
}