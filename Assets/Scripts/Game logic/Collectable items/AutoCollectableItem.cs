using UnityEngine;

namespace Game_logic.Collectable_items
{
    [RequireComponent(typeof(BoxCollider))]
    public class AutoCollectableItem: BaseCollectableItem
    {
        [SerializeField] private BoxCollider trigger;

        private void OnEnable()
        {
            rb.isKinematic = false;
            trigger.enabled = false;
            amount = info.maxItemsInSlot;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Inventory.TryToAdd(info, amount, out int restAmount) is false)
            {
                Debug.Log($"the element is not completely added, the rest amount is:{restAmount}");
                amount = restAmount;
                return;
            }
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision other)
        {
            rb.isKinematic = true;
            trigger.enabled = true;
        }
    }
}