using UnityEngine;

namespace Infrastructure
{
    public interface IPrefabService
    {
        GameObject GetWeaponPrefabByName(string name);
        public GameObject GetCrosshairPrefabByType(CrosshairType type);
    }
}