public interface IInventorySlot
{
    InventoryItemInfo ItemInfo { get; set; }
    int Amount { get; set; }
    
    bool IsFull { get; }
    bool IsEmpty { get; }

    void Clear();

}