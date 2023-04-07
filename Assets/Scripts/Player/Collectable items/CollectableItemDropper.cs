using MemoryPools;
using UnityEngine;
using Zenject;

namespace Player.Collectable_items
{
    public class CollectableItemDropper: MonoBehaviour
    {
        private CollectableItemsPool _pool;

        [Inject]
        private void Construct(CollectableItemsPool pool) => 
            _pool = pool;

        public void DropItem(InventoryItemInfo info, int amount)
        {
            var item = _pool.Spawn(info);

            item.info = info;
            item.amount = amount;

            item.transform.position = transform.position;
            item.rb.AddForce((transform.forward + transform.up) * 2f, ForceMode.Impulse);
        }
    }
}