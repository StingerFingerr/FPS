using System;
using System.Collections.Generic;
using Infrastructure;

public interface IInventory: IProgressReader, IProgressWriter, IProgressInitializer
{
    event Action OnInventoryStateChanged;

    bool TryToAdd(InventoryItemInfo itemInfo, int amount);
    List<InventorySlot> GetAllSlots();
    int GetAmountOfItems(InventoryItemInfo itemInfo);
    void MoveItemFromSlotToSlot(IInventorySlot fromSlot, IInventorySlot toSlot);
    bool TryToAddIntoSlot(IInventorySlot slot, InventoryItemInfo itemInfo, int count = 1);

}
