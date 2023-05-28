using System.Collections.Generic;
using System.Linq;
using Game_logic.Collectable_items;
using Zenject;

namespace MemoryPools
{
    public class CollectableItemsPool
    {
        private CollectableItemAbstractFactory _factory;

        private List<BaseCollectableItem> _itemsPool = new(30);

        [Inject]
        private void Construct(CollectableItemAbstractFactory factory) => 
            _factory = factory;

        public BaseCollectableItem Spawn(InventoryItemInfo info)
        {
            var item = _itemsPool.FirstOrDefault(i =>
                i.info.type == info.type && 
                i.info.secondaryType == info.secondaryType&&
                i.gameObject.activeSelf is false);

            if (item is null)            
                item = CreateNewItem(info);

            item.gameObject.SetActive(true);
            return item;
        }
        
        private BaseCollectableItem CreateNewItem(InventoryItemInfo info)
        {
            var newItem = _factory.Create(info);
            _itemsPool.Add(newItem);
            return newItem;
        }
    }
}