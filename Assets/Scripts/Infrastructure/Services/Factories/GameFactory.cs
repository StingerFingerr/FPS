using Infrastructure;
using Player;
using Player.Inputs;
using Prefab_service;
using UI.Game;
using UnityEngine;
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
        _diContainer.BindInstance(player.GetComponent<PlayerInputs>());
        
        return player;
    }

    public GameUI CreateGameUI()
    {
        var prefab = _prefabService.GetGameUIPrefab();
        var ui = _diContainer.InstantiatePrefabForComponent<GameUI>(prefab);

        _diContainer.BindInterfacesAndSelfTo<GameUI>().FromInstance(ui);
        
        return ui;
    }


}