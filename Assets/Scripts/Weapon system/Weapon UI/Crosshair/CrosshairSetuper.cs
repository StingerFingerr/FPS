using System;
using Player;
using UnityEngine;
using Weapons;
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
        FirstPersonController player, 
        WeaponHolder weaponHolder,
        DynamicCrosshairBase.CachedFactory crosshairCachedFactory)
    {
        _weaponHolder = weaponHolder;
        _player = player;
        _crosshairCachedFactory = crosshairCachedFactory;
    }

    private void OnEnable() => 
        _weaponHolder.OnWeaponSwitched += SetNewCrosshairFor;

    private void OnDisable() => 
        _weaponHolder.OnWeaponSwitched -= SetNewCrosshairFor;

    private void Update()
    {
        if (_crosshair is not null)
        {
            _crosshair.OnMove(_player.IsMoving);
            _crosshair.OnLook(_player.IsLooking);
        }
    }

    private void SetNewCrosshairFor(WeaponBase weapon, int index)
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
        _crosshair.transform.SetAsFirstSibling();
        
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

    private void OnAim(bool isAiming)
    {
        if(isAiming is false)
            _crosshair.Activate();
        _crosshair.OnAim(isAiming);
    }

    private void OnShot(Vector2 recoil) => 
        _crosshair.OnShot();

    private void Subscribe()
    {
        _weapon.OnShot += OnShot;
        _weapon.OnAiming += OnAim;

        _weapon.OnStartReloading += Hide;
        _weapon.OnEndReloading += Show;
    }

    private void Unsubscribe()
    {
        _weapon.OnShot -= OnShot;
        _weapon.OnAiming -= OnAim;
        
        _weapon.OnStartReloading -= Hide;
        _weapon.OnEndReloading -= Show;
    }

    private void Show()
    {
        if(_weapon.IsAiming is false)
            _crosshair.Activate();
    }

    private void Hide() => 
        _crosshair.Deactivate();
}