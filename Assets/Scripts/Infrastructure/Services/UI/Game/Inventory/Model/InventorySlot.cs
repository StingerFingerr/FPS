using System;

[Serializable]
public class InventorySlot: IInventorySlot
{
    private InventoryItemInfo _itemInfo;
    private int _amount;
    
    public InventoryItemInfo ItemInfo { get => _itemInfo; set => _itemInfo = value; }
    public int Amount { get => _amount; set => _amount = value; }
    public bool IsFull => IsEmpty is false && Amount == ItemInfo.maxItemsInSlot;
    public bool IsEmpty => ItemInfo is null && _amount == 0;
    
    public void Clear()
    {
        _itemInfo = null;
        _amount = 0;
    }
}