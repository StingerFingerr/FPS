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
    
    public void SetNewWeapon(WeaponBase weapon)
    {
        SwitchCurrentWeapon?.Invoke(weapon);
    }

    private void OnScroll(InputValue inputValue)
    {
        float scroll = inputValue.Get<float>();
        if(scroll==0)
            return;

        weapons[_weaponIndex]?.Hide();
        
        if (scroll > 0)
        {
            _weaponIndex++;
            if (_weaponIndex == weapons.Length)
                _weaponIndex = 0;
        }else if (scroll < 0)
        {
            _weaponIndex--;
            if (_weaponIndex < 0)
                _weaponIndex = weapons.Length - 1;
        }

        weapons[_weaponIndex]?.Show();
        SwitchCurrentWeapon?.Invoke(weapons[_weaponIndex]);
    }

}
