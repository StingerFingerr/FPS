using System;
using System.Collections.Generic;
using Game_logic;

public class GridInventory: IInventory
{
    public event Action OnInventoryStateChanged;
    
    private List<InventorySlot> _slots;
    
    public void Load(Progress progress) => 
        _slots = progress.inventorySlots;

    public void Save(Progress progress) => 
        progress.inventorySlots = _slots;

    public void InitializeProgressData()
    {
        _slots = new List<InventorySlot>(30);

        for (int i = 0; i < 30; i++)        
            _slots.Add(new InventorySlot());
    }

    public bool TryToAdd(InventoryItemInfo itemInfo, int amount, out int restAmount)
    {
        restAmount = 0;
        if (itemInfo.isStackable is false)
        {
            if (TryToAddIntoEmptySlot(itemInfo, out int rest1))
                return true;
            else
                restAmount = rest1;
        }

        if (TryToAddIntoSlotWithSameItem(itemInfo, amount, out int rest2))
        {
            restAmount = 0;
            return true;
        }
        else
        {
            restAmount = rest2;
        }

        if (TryToAddIntoEmptySlot(itemInfo, out int rest3, restAmount))
        {
            restAmount = 0;
            return true;
        }
        else
        {
            restAmount = rest3;
        }

        return false;
    }

    public bool TryToAddIntoSlot(IInventorySlot slot, InventoryItemInfo itemInfo, out int restAmount, int count = 1)
    {
        restAmount = 0;
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
        
        if (TryToAdd(itemInfo, amountLeft, out int rest))
        {
            restAmount = 0;
            return true;
        }
        else
        {
            restAmount = rest;
            return false;
        }
    }

    public void RemoveFromSlot(IInventorySlot slot)
    {
        slot.Amount = 0;
        slot.ItemInfo = null;
        
        OnInventoryStateChanged?.Invoke();
    }

    public int RemoveItemAmount(InventoryItemInfo info, int amount)
    {
        int availableAmount = 0;

        while (availableAmount < amount)
        {
            var slot = _slots.FindLast(slot =>
                slot.IsEmpty is false &&
                slot.ItemInfo.type == info.type &&
                slot.ItemInfo.secondaryType == info.secondaryType);
            
            if (slot is null)
                break;

            int requiredAmount = amount - availableAmount;
            bool enough = slot.Amount >= requiredAmount;
            if (enough)
            {
                availableAmount += requiredAmount;
                slot.Amount -= requiredAmount;
                if(slot.Amount <= 0)
                    slot.Clear();
                break;
            }
            availableAmount += slot.Amount;
            slot.Clear();
        }

        OnInventoryStateChanged?.Invoke();
        return availableAmount;
    }

    private bool TryToAddIntoEmptySlot(InventoryItemInfo itemInfo, out int restAmount, int amount = 1)
    {
        var emptySlot = _slots.Find(slot => slot.IsEmpty);

        if (emptySlot is not null)
        {
            if (TryToAddIntoSlot(emptySlot, itemInfo, out int rest, amount))
            {
                InvokeSuccessfulAdding(itemInfo, amount);
                restAmount = 0;
                return true;
            }
            else
            {
                restAmount = rest;
                return false;
            }
        }

        restAmount = amount;
        return false;
    }

    private bool TryToAddIntoSlotWithSameItem(InventoryItemInfo itemInfo, int amount, out int restAmount)
    {
        var slotWithSameItem = _slots.Find(slot =>
            slot.IsEmpty is false &&
            slot.ItemInfo.type == itemInfo.type &&
            slot.IsFull is false
        );

        if (slotWithSameItem is not null)
        {
            if (TryToAddIntoSlot(slotWithSameItem, itemInfo, out int rest, amount))
            {
                InvokeSuccessfulAdding(itemInfo, amount);
                restAmount = 0;
                return true;
            }
            else
            {
                restAmount = rest;
                return false;
            }
        }

        restAmount = amount;
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
                TryToAddIntoEmptySlot(info,  out int restAmount);
                return;
            }
            if (fromSlot.ItemInfo.type == toSlot.ItemInfo.type)
            {
                InventoryItemInfo itemInfo = fromSlot.ItemInfo;
                int amount = fromSlot.Amount;
            
                fromSlot.Clear();
            
                TryToAddIntoSlot(toSlot, itemInfo, out int restA, amount);
            
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