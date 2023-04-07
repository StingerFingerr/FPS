using UnityEngine;
using Zenject;

namespace Game_logic.Collectable_items
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseCollectableItem: MonoBehaviour
    {
        public class Factory : PlaceholderFactory<InventoryItemInfo, BaseCollectableItem>
        { }
        
        public InventoryItemInfo info;
        public int amount;

        protected IInventory Inventory;
        public Rigidbody rb;

        [Inject]
        private void Construct(IInventory inventory)
        {
            Inventory = inventory;
            rb = GetComponent<Rigidbody>();
        }
    }
}