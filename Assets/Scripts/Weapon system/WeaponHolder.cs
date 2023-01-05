using System;
using System.Collections;
using Game_logic;
using Infrastructure;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;
using Weapons;

public class WeaponHolder : MonoBehaviour, IProgressReader, IProgressWriter
{
    public WeaponSlot[] weaponSlots;
    public float switchWeaponTimeout = .5f;
    public event Action<WeaponBase> SwitchCurrentWeapon;

    private WeaponBase CurrentWeapon
    {
        get => weaponSlots[_weaponIndex].weapon;
        set => weaponSlots[_weaponIndex].weapon = value;
    }
    private int _weaponIndex = 0;
    private bool _canSwitchWeapon = true;

    private void Start()
    {
        SwitchCurrentWeapon?.Invoke(CurrentWeapon);
    }

    public void SetNewWeapon(WeaponBase weapon)
    {
        CurrentWeapon?.ThrowAway();

        CurrentWeapon = weapon;
        CurrentWeapon.transform.parent = weaponSlots[_weaponIndex].transform;
        
        SwitchCurrentWeapon?.Invoke(CurrentWeapon);
    }

    private void OnThrowAway(InputValue inputValue)
    {
        CurrentWeapon?.ThrowAway();
        CurrentWeapon = null;
        
        SwitchCurrentWeapon?.Invoke(null);
    }
    
    private void OnScroll(InputValue inputValue)
    {
        float scroll = inputValue.Get<float>();
        if(scroll==0)
            return;

        if(_canSwitchWeapon is false)
            return;
        
        CurrentWeapon?.Hide();

        if (scroll > 0)
            ScrollForward();
        else if (scroll < 0)        
            ScrollBackward();

        StartCoroutine(StartSwitchTimeout(switchWeaponTimeout));
        
        CurrentWeapon?.Show();
        SwitchCurrentWeapon?.Invoke(CurrentWeapon);
    }

    private void ScrollForward()
    {
        _weaponIndex++;
        if (_weaponIndex == weaponSlots.Length)
            _weaponIndex = 0;
    }

    private void ScrollBackward()
    {
        _weaponIndex--;
        if (_weaponIndex < 0)
            _weaponIndex = weaponSlots.Length - 1;
    }

    private IEnumerator StartSwitchTimeout(float timeOut)
    {
        _canSwitchWeapon = false;
        yield return new WaitForSeconds(timeOut);
        _canSwitchWeapon = true;
    }

    public void Load(Progress progress)
    {
        _weaponIndex = progress.weaponHolderInfo.currentWeaponIndex;
    }

    public void Save(Progress progress)
    {
        progress.weaponHolderInfo.currentWeaponIndex = _weaponIndex;
    }
}
