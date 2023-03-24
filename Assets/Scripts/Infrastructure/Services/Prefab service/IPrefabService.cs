using UnityEngine;

namespace Prefab_service
{
    public interface IPrefabService
    {
        public GameObject GetWeaponPrefabByName(string name);
        public GameObject GetCrosshairPrefabByType(CrosshairType type);
        public GameObject GetPlayerPrefab();

        public GameObject GetGameUIPrefab();
    }
}