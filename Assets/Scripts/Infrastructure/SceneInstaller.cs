using Player;
using UnityEngine;
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
        }

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