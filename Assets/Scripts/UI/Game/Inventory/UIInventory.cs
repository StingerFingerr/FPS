using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class UIInventory: BaseWindow
{
    [SerializeField] private List<UIInventorySlot> uiSlots;
    [SerializeField] private Canvas canvas;

    public InventoryItemInfo bulletsInfo;
    public InventoryItemInfo shotgunBulletsInfo;
    public InventoryItemInfo medKitLittleInfo;
    public InventoryItemInfo medKitBigInfo;
    public InventoryItemInfo silencerInfo;
    public InventoryItemInfo compensatorInfo;
    public InventoryItemInfo extendedMagazineInfo;

    public override bool IsOpened => _inventoryIsOpen;

    private IInventory _inventory;

    private bool _inventoryIsOpen;


    [Inject]
    private void Construct(IInventory inventory)
    {
        _inventory = inventory;
    }

    private void Start()
    {
        SetupUIInventorySlots();
        RefreshSlots();
        Close();
    }

    private void FillInventory()
    {
        _inventory.TryToAdd(bulletsInfo, 100, out int restAmount);
        _inventory.TryToAdd(shotgunBulletsInfo, 30, out int asdfv);
        _inventory.TryToAdd(compensatorInfo, 3, out int ras);
        _inventory.TryToAdd(silencerInfo, 3, out int asdfa);
        _inventory.TryToAdd(medKitLittleInfo, 12, out int fsd);
        _inventory.TryToAdd(extendedMagazineInfo, 12, out int fasd);
    }

    private void OnEnable()
    {
        _inventory.OnInventoryStateChanged += RefreshSlots;
    }

    private void OnDisable()
    {
        _inventory.OnInventoryStateChanged -= RefreshSlots;
    }

    public override void Open()
    {
        _inventoryIsOpen = true;
        canvas.enabled = true;
    }

    public override void Close()
    {
        _inventoryIsOpen = false;
        canvas.enabled = false;
    }

    private void SetupUIInventorySlots()
    {
        var slots = _inventory.GetAllSlots();
        for (int i = 0; i < uiSlots.Count; i++)        
            uiSlots[i].SetupInventorySlot(slots[i]);
    }

    private void RefreshSlots() => 
        uiSlots.ForEach(slot => slot.Refresh());
}