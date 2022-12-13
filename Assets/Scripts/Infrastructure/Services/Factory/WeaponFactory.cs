using Infrastructure;
using Unity.Mathematics;
using UnityEngine;
using Weapon;
using Zenject;

public class WeaponFactory: IFactory<string, WeaponBase>
{
    private DiContainer _container;
    private IPrefabService _prefabService;

    public WeaponFactory (
        DiContainer container,
        IPrefabService prefabService)
    {
        _container = container;
        _prefabService = prefabService;
    }
    
    
    public WeaponBase Create(string name)
    {
        var prefab = _prefabService.GetWeaponPrefabByName(name);
        return _container.InstantiatePrefabForComponent<WeaponBase>(prefab);
    }
}