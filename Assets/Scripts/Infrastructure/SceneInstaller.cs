using System.Collections.Generic;
using Player;
using UnityEngine;
using Weapon_system;
using Zenject;

namespace Infrastructure
{
    public class SceneInstaller: MonoInstaller
    {
        public GameObject playerPrefab;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindSceneProgressService();

            Container.BindInterfacesTo<WeaponSlot>().FromMethodMultiple(GetWeaponSlots);
        }

        private IEnumerable<WeaponSlot> GetWeaponSlots(InjectContext arg) => 
            FindObjectsOfType<WeaponSlot>();

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
    }
}