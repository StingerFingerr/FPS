using MemoryPools;
using UnityEngine;
using Zenject;

namespace Game_logic.Spawners
{
    public class InventoryItemSpawner: MonoBehaviour
    {
        [SerializeField] private InventoryItemInfo itemInfo;

        private CollectableItemsPool _pool;
        
        [Inject]
        private void Construct(CollectableItemsPool pool) => 
            _pool = pool;


        public void Spawn()
        {
            var item = _pool.Spawn(itemInfo);
            item.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}