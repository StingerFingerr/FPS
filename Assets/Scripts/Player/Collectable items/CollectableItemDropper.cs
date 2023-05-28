using System;
using MemoryPools;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Player.Collectable_items
{
    public class CollectableItemDropper: MonoBehaviour
    {
        [SerializeField] private bool randomDropDirection;
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
            item.rb.AddForce(GetDirection(), ForceMode.Impulse);
        }

        private Vector3 GetDirection() => 
            randomDropDirection 
                ? GetRandomDirection() 
                : GetForwardDirection();

        private Vector3 GetRandomDirection() =>
            Vector3.up * 5 + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        private Vector3 GetForwardDirection() => 
            (transform.forward + transform.up) * 2f;
    }
}