using System.Collections.Generic;
using System.Linq;
using Prefab_service;
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
}