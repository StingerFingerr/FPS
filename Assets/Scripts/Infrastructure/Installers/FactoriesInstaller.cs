using Game_logic.Collectable_items;
using MemoryPools;
using Weapons;
using Zenject;

public class FactoriesInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        BindGameFactory();
        BindWeaponFactory();

        //BindCollectableItemsFactory();
        Container.Bind<CollectableItemAbstractFactory>().AsSingle();
        BindCollectableItemsPool();
    }

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