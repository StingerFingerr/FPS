using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using UnityEngine;

[CreateAssetMenu(menuName = "Services/PrefabService", fileName = "PrefabService")]
public class PrefabService: ScriptableObject, IPrefabService
{
    public List<GameObject> weaponsPrefabs;
        
        
    public GameObject GetWeaponPrefabByName(string name) => 
        weaponsPrefabs.FirstOrDefault(x => x.name.Equals(name));
}