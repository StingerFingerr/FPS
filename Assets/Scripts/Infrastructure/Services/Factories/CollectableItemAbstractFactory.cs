using Game_logic.Collectable_items;
using Zenject;

public class CollectableItemAbstractFactory
{
    private IPrefabService _prefabService;
    private DiContainer _diContainer;

    public CollectableItemAbstractFactory(IPrefabService prefabService, DiContainer diContainer)
    {
        _prefabService = prefabService;
        _diContainer = diContainer;
    }

    public BaseCollectableItem Create(InventoryItemInfo info) => 
        _diContainer.InstantiatePrefabForComponent<BaseCollectableItem>(_prefabService.GetCollectableItemPrefab(info));
}