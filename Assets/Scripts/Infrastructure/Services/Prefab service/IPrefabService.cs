using UnityEngine;

namespace Infrastructure
{
    public interface IPrefabService
    {
        public GameObject GetWeaponPrefabByName(string name);
        public GameObject GetCrosshairPrefabByType(CrosshairType type);
        public GameObject GetPlayerPrefab();

        public GameObject GetCrosshairSetuper();
    }
}