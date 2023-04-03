using UI.Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot: UISlot
{
    public IInventorySlot InventorySlot { get; private set; }

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
            Inventory.TryToAddIntoSlot(InventorySlot, uiAttachmentSlot.ItemInfo);
            uiAttachmentSlot.Clear();

        }
    }

    public void Refresh()
    {
        if(InventorySlot.IsEmpty)
            draggableItem.Hide();
        else
            draggableItem.SetNewInfo(InventorySlot.ItemInfo, InventorySlot.Amount);
    }

    public void SetupInventorySlot(IInventorySlot slot) => 
        InventorySlot = slot;
}