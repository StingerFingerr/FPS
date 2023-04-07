using Player.Interaction;
using UnityEngine;

namespace Game_logic.Collectable_items
{
    public class PickableCollectableItem: BaseCollectableItem
    {
        [SerializeField] private HoverableObject hoverableObject;
        [SerializeField] private BoxCollider trigger;

        public void Pickup()
        {
            if (Inventory.TryToAdd(info, amount, out int restAmount))            
                gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            rb.isKinematic = false;
            trigger.enabled = false;
            hoverableObject.enabled = false;
            amount = info.maxItemsInSlot;
        }

        private void OnCollisionEnter(Collision other)
        {
            rb.isKinematic = true;
            trigger.enabled = true;
            hoverableObject.enabled = true;
        }
    }
}