using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapon;

public class WeaponHolder : MonoBehaviour
{
    public WeaponBase[] weapons;

    public event Action<WeaponBase> SwitchCurrentWeapon;
    public WeaponBase CurrentWeapon => weapons[0];

    private int _weaponIndex = 0;

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

        weapons[_weaponIndex]?.Hide();

        if (scroll > 0)
            ScrollForward();
        else if (scroll < 0)        
            ScrollBackward();

        weapons[_weaponIndex]?.Show();
        SwitchCurrentWeapon?.Invoke(weapons[_weaponIndex]);
    }

    private void ScrollBackward()
    {
        _weaponIndex--;
        if (_weaponIndex < 0)
            _weaponIndex = weapons.Length - 1;
    }

    private void ScrollForward()
    {
        _weaponIndex++;
        if (_weaponIndex == weapons.Length)
            _weaponIndex = 0;
    }
}
