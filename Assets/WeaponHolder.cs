using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;

public class WeaponHolder : MonoBehaviour
{
    public WeaponBase[] weapons;
    public float switchWeaponTimeout = .5f;
    public event Action<WeaponBase> SwitchCurrentWeapon;
    
    private int _weaponIndex = 0;
    private bool _canSwitchWeapon = true;

    private void Start()
    {
        if (weapons[_weaponIndex] is not null)
            SwitchCurrentWeapon?.Invoke(weapons[_weaponIndex]);
    }

    public void SetNewWeapon(WeaponBase weapon)
    {
        weapons[_weaponIndex]?.ThrowAway();

        weapons[_weaponIndex] = weapon;
        weapons[_weaponIndex].transform.parent = transform;
        weapons[_weaponIndex].Take();
    }

    private void OnThrowAway(InputValue inputValue)
    {
        weapons[_weaponIndex]?.ThrowAway();
        weapons[_weaponIndex] = null;
        SwitchCurrentWeapon?.Invoke(null);
    }
    
    private void OnScroll(InputValue inputValue)
    {
        float scroll = inputValue.Get<float>();
        if(scroll==0)
            return;

        if(_canSwitchWeapon is false)
            return;
        
        weapons[_weaponIndex]?.Hide();

        if (scroll > 0)
            ScrollForward();
        else if (scroll < 0)        
            ScrollBackward();

        StartCoroutine(StartSwitchTimeout(switchWeaponTimeout));
        
        weapons[_weaponIndex]?.Show();
        SwitchCurrentWeapon?.Invoke(weapons[_weaponIndex]);
    }

    private void ScrollForward()
    {
        _weaponIndex++;
        if (_weaponIndex == weapons.Length)
            _weaponIndex = 0;
    }

    private void ScrollBackward()
    {
        _weaponIndex--;
        if (_weaponIndex < 0)
            _weaponIndex = weapons.Length - 1;
    }

    private IEnumerator StartSwitchTimeout(float timeOut)
    {
        _canSwitchWeapon = false;
        yield return new WaitForSeconds(timeOut);
        _canSwitchWeapon = true;
    } 
}
