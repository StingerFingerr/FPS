using System;
using System.Collections.Generic;
using Game_logic;
using UnityEngine;

public class GridInventory: IInventory
{
    public event Action OnInventoryStateChanged;
    
    private List<InventorySlot> _slots;
    
    public void Load(Progress progress)
    {
        _slots = progress.inventorySlots;
        Debug.Log($"load: {_slots.Count} slots");
    }

    public void Save(Progress progress)
    {
        progress.inventorySlots = _slots;
    }

    public void InitializeProgressData()
    {
        _slots = new List<InventorySlot>(30);

        for (int i = 0; i < 30; i++)        
            _slots.Add(new InventorySlot());
    }

    public bool TryToAdd(InventoryItemInfo itemInfo, int amount)
    {
        if (itemInfo.isStackable is false)
        {
            if (TryToAddIntoEmptySlot(itemInfo))
                return true;
        }
        
        if (TryToAddIntoSlotWithSameItem(itemInfo, amount)) 
            return true;

        if (TryToAddIntoEmptySlot(itemInfo, amount)) 
            return true;
        
        return false;
    }

    public bool TryToAddIntoSlot(IInventorySlot slot, InventoryItemInfo itemInfo, int count = 1)
    {
        if (itemInfo.isStackable is false)
        {
            slot.ItemInfo = itemInfo;
            slot.Amount = 1;
            InvokeSuccessfulAdding(itemInfo, 1);
            return true;
        }
        
        var fits = slot.Amount + count <= itemInfo.maxItemsInSlot;

        if (fits)
        {
            slot.Amount += count;
            slot.ItemInfo = itemInfo;
            InvokeSuccessfulAdding(itemInfo, count);
            return true;
        }

        var amountToAdd = itemInfo.maxItemsInSlot - slot.Amount;
        var amountLeft = count - amountToAdd;
        
        slot.Amount += amountToAdd;
        slot.ItemInfo = itemInfo;

        InvokeSuccessfulAdding(itemInfo, amountToAdd);
        
        return TryToAdd(itemInfo, amountLeft);
    }

    private bool TryToAddIntoEmptySlot(InventoryItemInfo itemInfo, int amount = 1)
    {
        var emptySlot = _slots.Find(slot => slot.IsEmpty);

        if (emptySlot is not null)
        {
            if (TryToAddIntoSlot(emptySlot, itemInfo, amount))
            {
                InvokeSuccessfulAdding(itemInfo, amount);
                return true;
            }
        }

        return false;
    }

    private bool TryToAddIntoSlotWithSameItem(InventoryItemInfo itemInfo, int amount)
    {
        var slotWithSameItem = _slots.Find(slot =>
            slot.IsEmpty is false &&
            slot.ItemInfo.type == itemInfo.type &&
            slot.IsFull is false
        );

        if (slotWithSameItem is not null)
        {
            if (TryToAddIntoSlot(slotWithSameItem, itemInfo, amount))
            {
                InvokeSuccessfulAdding(itemInfo, amount);
                return true;
            }
        }

        return false;
    }

    public List<InventorySlot> GetAllSlots() => 
        _slots;

    public int GetAmountOfItems(InventoryItemInfo itemInfo)
    {
        int count = 0;
        foreach (var inventorySlot in _slots.FindAll(slot => slot.ItemInfo == itemInfo))
        {
            count += inventorySlot.Amount;
        }

        return count;
    }

    public void MoveItemFromSlotToSlot(IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (toSlot.IsEmpty is false)
        {
            if (fromSlot.ItemInfo.isStackable is false)
            {
                InventoryItemInfo info = fromSlot.ItemInfo;
                fromSlot.Clear();
                TryToAddIntoEmptySlot(info);
                return;
            }
            if (fromSlot.ItemInfo.type == toSlot.ItemInfo.type)
            {
                InventoryItemInfo itemInfo = fromSlot.ItemInfo;
                int amount = fromSlot.Amount;
            
                fromSlot.Clear();
            
                TryToAddIntoSlot(toSlot, itemInfo, amount);
            
                return;
            }
        }
        
        
        var fromSlotInfo = fromSlot.ItemInfo;
        var fromSlotAmount = fromSlot.Amount;

        fromSlot.ItemInfo = toSlot.ItemInfo;
        fromSlot.Amount = toSlot.Amount;

        toSlot.ItemInfo = fromSlotInfo;
        toSlot.Amount = fromSlotAmount;
        
        OnInventoryStateChanged?.Invoke();
    }

    private void InvokeSuccessfulAdding(InventoryItemInfo itemInfo, int amount)
    {
        OnInventoryStateChanged?.Invoke();
        //Debug.Log($"item: {info.name} has been successfully added to the inventory in amount: {amount}");
    }
}