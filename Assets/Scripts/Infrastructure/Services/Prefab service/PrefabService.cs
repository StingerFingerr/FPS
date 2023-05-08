using System.Collections.Generic;
using System.Linq;
using Game_logic.Collectable_items;
using UnityEngine;
using Weapons;

[CreateAssetMenu(menuName = "Services/create PrefabService", fileName = "PrefabService")]
public class PrefabService: ScriptableObject, IPrefabService
{
    public List<WeaponBase> weaponsPrefabs;
    public List<DynamicCrosshairBase> crosshairsPrefabs;
    public GameObject playerPrefab;
    public GameObject gameUIPrefab;
    public GameObject gameInventoryPrefab;
    public List<BaseCollectableItem> collectableItemsPrefabs;
    
    public GameObject bulletPrefab;
    
    public GameObject bulletImpactPrefab;
    public GameObject bloodyBulletImpactPrefab;
    
    public GameObject GetWeaponPrefabByName(string name) => 
        weaponsPrefabs.FirstOrDefault(x => x.name.Equals(name))?.gameObject;

    public GameObject GetCrosshairPrefabByType(CrosshairType type) => 
        crosshairsPrefabs.FirstOrDefault(x => x.type == type)?.gameObject;

    public GameObject GetPlayerPrefab() => 
        playerPrefab;

    public GameObject GetGameUIPrefab() => 
        gameUIPrefab;

    public GameObject GetInventoryUIPrefab() => 
        gameInventoryPrefab;

    public GameObject GetCollectableItemPrefab(InventoryItemInfo info) =>
        collectableItemsPrefabs.First(item =>
            item.info.type == info.type &&
            item.info.secondaryType == info.secondaryType).gameObject;

}