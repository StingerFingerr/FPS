using UnityEngine;

public interface IPrefabService
{
    public GameObject GetWeaponPrefabByName(string name);
    public GameObject GetCrosshairPrefabByType(CrosshairType type);
    public GameObject GetPlayerPrefab();

    public GameObject GetGameUIPrefab();
    public GameObject GetInventoryUIPrefab();
    public GameObject GetCollectableItemPrefab(InventoryItemInfo info);
}