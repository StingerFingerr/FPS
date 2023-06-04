using Player.Collectable_items;
using UI.Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UIInventorySlot: UISlot
{
    public IInventorySlot InventorySlot { get; private set; }

    private CollectableItemDropper _dropper;

    [Inject]
    private void Construct(CollectableItemDropper dropper) => 
        _dropper = dropper;

    public override void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<DraggableItem>();

        item.OnEndDrag(null);

        if (item.UIInventorySlot is UIInventorySlot uiInventorySlot)
        {
            Inventory.MoveItemFromSlotToSlot(uiInventorySlot.InventorySlot, InventorySlot);
            return;
        }

        if (item.UIInventorySlot is UIInventoryAttachmentSlot uiAttachmentSlot)
        {
            Inventory.TryToAddIntoSlot(InventorySlot, uiAttachmentSlot.ItemInfo, out int restAmount);
            uiAttachmentSlot.Clear();
        }
    }

    public override void Drop()
    {
        _dropper.DropItem(InventorySlot.ItemInfo, InventorySlot.Amount);
        draggableItem.OnPointerExit(null);
        Inventory.RemoveFromSlot(InventorySlot);
    }

    public override void Clear() => 
        Inventory.RemoveFromSlot(InventorySlot);

    public void Refresh()
    {
        if (InventorySlot is null)
            return;
        if(InventorySlot.IsEmpty)
            draggableItem.Hide();
        else
            draggableItem.SetNewInfo(InventorySlot.ItemInfo, InventorySlot.Amount);
    }

    public void SetupInventorySlot(IInventorySlot slot) => 
        InventorySlot = slot;
}