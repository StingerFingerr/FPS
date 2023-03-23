using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Prefab_service;
using UnityEngine;
using Weapon;
using Weapons;

[CreateAssetMenu(menuName = "Services/PrefabService", fileName = "PrefabService")]
public class PrefabService: ScriptableObject, IPrefabService
{
    public List<WeaponBase> weaponsPrefabs;
    public List<DynamicCrosshairBase> crosshairsPrefabs;
    public GameObject playerPrefab;
    public GameObject GameUIPrefab;
    
    public GameObject GetWeaponPrefabByName(string name) => 
        weaponsPrefabs.FirstOrDefault(x => x.name.Equals(name))?.gameObject;

    public GameObject GetCrosshairPrefabByType(CrosshairType type) => 
        crosshairsPrefabs.FirstOrDefault(x => x.type == type)?.gameObject;

    public GameObject GetPlayerPrefab() => 
        playerPrefab;

    public GameObject GetGameUIPrefab() => 
        GameUIPrefab;
}