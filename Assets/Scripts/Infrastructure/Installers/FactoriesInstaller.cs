using System;
using Game_logic.Collectable_items;
using MemoryPools;
using UnityEngine;
using Weapons;
using Zenject;

public class FactoriesInstaller: MonoInstaller
{
    [SerializeField] private PrefabService prefabs;
    public override void InstallBindings()
    {
        BindGameFactory();
        BindWeaponFactory();

        //BindCollectableItemsFactory();
        Container.Bind<CollectableItemAbstractFactory>().AsSingle();
        BindCollectableItemsPool();

        BindEnemyFactory();

        Container.BindFactory<Vector3, Action, BaseBossZombie, BaseBossZombie.Factory>()
            .FromPoolableMemoryPool<Vector3, Action, BaseBossZombie, BaseBossZombie.Pool>(poolBinder => poolBinder
                .WithInitialSize(1)
                .FromMethod(CreateRandomEnemyBoss));
    }


    private BaseBossZombie CreateRandomEnemyBoss(DiContainer arg)
    {
        return Container.InstantiatePrefabForComponent<BaseBossZombie>(prefabs.GetRandomEnemyBossPrefab());
    }

    private void BindEnemyFactory()
    {
        Container.BindFactory<Vector3, Action, BaseZombie, BaseZombie.Factory>()
            .FromPoolableMemoryPool<Vector3, Action, BaseZombie, BaseZombie.Pool>(poolBinder=> poolBinder
                .WithInitialSize(10)
                .FromMethod(CreateRandomEnemy));
        
        
    }

    private BaseZombie CreateRandomEnemy(DiContainer arg) => 
        Container.InstantiatePrefabForComponent<BaseZombie>(prefabs.GetRandomEnemyPrefab());
    

    private void BindCollectableItemsPool() => 
        Container.Bind<CollectableItemsPool>().AsSingle();

    //private void BindCollectableItemsFactory() => 
    //    Container
    //        .BindFactory<InventoryItemInfo, BaseCollectableItem, BaseCollectableItem.Factory>()
    //        .FromFactory<CollectableItemAbstractFactory>()
    //        .NonLazy();

    private void BindGameFactory() => 
        Container.Bind<GameFactory>().AsSingle();

    private void BindWeaponFactory() => 
        Container.BindFactory<string, WeaponBase, WeaponBase.Factory>().FromFactory<WeaponFactory>();
}