using Player.Collectable_items;
using Zenject;

public class GameFactory: IFactory
{
    private readonly IPrefabService _prefabService;
    private readonly DiContainer _diContainer;

    public GameFactory(IPrefabService prefabService, DiContainer diContainer)
    {
        _prefabService = prefabService;
        _diContainer = diContainer;
    }

    public FirstPersonController CreatePlayer()
    {
        var prefab = _prefabService.GetPlayerPrefab();
        var player = _diContainer.InstantiatePrefabForComponent<FirstPersonController>(prefab);

        _diContainer.BindInterfacesAndSelfTo<FirstPersonController>().FromInstance(player).AsSingle();
        _diContainer.BindInstance(player.GetComponent<PlayerInputs>()).AsSingle();
        _diContainer.BindInterfacesAndSelfTo<PlayerHealth>().FromInstance(player.GetComponent<PlayerHealth>()).AsSingle();
        _diContainer.BindInstance(player.GetComponentInChildren<CollectableItemDropper>()).AsSingle();
        
        return player;
    }

    public GameUI CreateGameUI()
    {
        var prefab = _prefabService.GetGameUIPrefab();
        var ui = _diContainer.InstantiatePrefabForComponent<GameUI>(prefab);
        _diContainer.BindInterfacesAndSelfTo<GameUI>().FromInstance(ui).AsSingle();
        return ui;
    }

    public UIInventory CreateInventoryUI()
    {
        var prefab = _prefabService.GetInventoryUIPrefab();
        var ui = _diContainer.InstantiatePrefabForComponent<UIInventory>(prefab);
        _diContainer.BindInstance(ui);
        return ui;
    }


}