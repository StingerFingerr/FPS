using UnityEngine;

namespace Infrastructure
{
    public interface IPrefabService
    {
        GameObject GetWeaponPrefabByName(string name);
    }
}