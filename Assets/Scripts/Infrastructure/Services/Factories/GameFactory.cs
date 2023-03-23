using Infrastructure;
using Player;
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

        _diContainer.BindInterfacesAndSelfTo<FirstPersonController>().FromInstance(player);
        
        return player;
    }

    public CrosshairSetuper CreateCrosshairSetuper()
    {
        var prefab = _prefabService.GetCrosshairSetuper();
        return _diContainer.InstantiatePrefabForComponent<CrosshairSetuper>(prefab);
    }


}