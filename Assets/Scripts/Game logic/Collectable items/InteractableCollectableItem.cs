using Player.Interaction;
using UnityEngine;

namespace Game_logic.Collectable_items
{
    public class InteractableCollectableItem: InteractableObject
    {
        [SerializeField] private PickableCollectableItem pickableItem;
        
        protected override void Interact() => 
            pickableItem.Pickup();
    }
}