using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;
using Weapon_system;

public class WeaponHolder : MonoBehaviour
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
        if (CurrentWeapon is not null)
            SwitchCurrentWeapon?.Invoke(CurrentWeapon);
    }

    public void SetNewWeapon(WeaponBase weapon)
    {
        CurrentWeapon?.ThrowAway();

        CurrentWeapon = weapon;
        CurrentWeapon.transform.parent = transform;
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
}
