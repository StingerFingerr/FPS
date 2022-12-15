using System.Collections.Generic;
using Infrastructure;
using Player;
using UnityEngine;
using Zenject;

public class SceneInstaller: MonoInstaller
{
    public GameObject playerPrefab;
        
    public override void InstallBindings()
    {
        BindPlayer();
        BindSceneProgressService();
        BindWeaponSlots();
        BindWeaponHolder();
    }

    private void BindWeaponHolder() => 
        Container.BindInterfacesTo<WeaponHolder>().FromMethod(GetWeaponHolder);

    private void BindWeaponSlots() => 
        Container.BindInterfacesTo<WeaponSlot>().FromMethodMultiple(GetWeaponSlots);

    private void BindSceneProgressService()
    {
        Container.Bind<ISceneProgressService>().To<SceneProgressService>().AsSingle().NonLazy();
        Container.BindInterfacesTo<ISceneProgressService>().FromResolve();
    }

    private void BindPlayer()
    {
        Container
            .BindInterfacesAndSelfTo<FirstPersonController>()
            .FromComponentInNewPrefab(playerPrefab)
            .AsSingle()
            .NonLazy();
    }

    private WeaponHolder GetWeaponHolder(InjectContext arg) => 
        FindObjectOfType<WeaponHolder>();

    private IEnumerable<WeaponSlot> GetWeaponSlots(InjectContext arg) => 
        FindObjectsOfType<WeaponSlot>();
}