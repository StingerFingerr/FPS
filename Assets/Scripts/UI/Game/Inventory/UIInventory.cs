using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Player.Inputs;
using UnityEngine.InputSystem;

public class UIInventory: MonoBehaviour
{
    [SerializeField] private List<UIInventorySlot> uiSlots;
    [SerializeField] private Canvas canvas;

    public InventoryItemInfo bulletsInfo;
    public InventoryItemInfo shotgunBulletsInfo;
    public InventoryItemInfo medKitLittleInfo;
    public InventoryItemInfo medKitBigInfo;
    public InventoryItemInfo silencerInfo;
    public InventoryItemInfo compensatorInfo;

    private IInventory _inventory;
    private PlayerInputs _playerInputs;
    private PlayerInput _playerInput;

    private bool _inventoryIsOpen;
    
    [Inject]
    private void Construct(IInventory inventory, PlayerInputs playerInputs)
    {
        _inventory = inventory;
        _playerInputs = playerInputs;
        _playerInput = playerInputs.GetComponent<PlayerInput>();
    }

    private void Start()
    {
        SetupUIInventorySlots();
        RefreshSlots();

        _inventory.TryToAdd(bulletsInfo, 300, out int restAmount);
        _inventory.TryToAdd(shotgunBulletsInfo, 30, out int asdfv);
        _inventory.TryToAdd(medKitBigInfo, 3, out int r);
        _inventory.TryToAdd(compensatorInfo, 3, out int ras);
        _inventory.TryToAdd(silencerInfo, 3, out int asdfa);
        _inventory.TryToAdd(medKitLittleInfo, 12, out int fsd);
        _inventory.TryToAddIntoSlot(_inventory.GetAllSlots()[25], compensatorInfo, out int rest);
        //FillInventory();

        CloseInventory();
    }

    private void FillInventory()
    {
        _inventory.TryToAdd(bulletsInfo, 30, out int restAmount);
        //_inventory.TryToAddIntoSlot(_inventory.GetAllSlots()[10], bulletsInfo, 5);
        //_inventory.TryToAddIntoSlot(_inventory.GetAllSlots()[12], medKitLittleInfo, 5);
        //_inventory.TryToAddIntoSlot(_inventory.GetAllSlots()[14], bulletsInfo, 5);
        //_inventory.TryToAddIntoSlot(_inventory.GetAllSlots()[15], medKitLittleInfo, 10);
        //_inventory.TryToAddIntoSlot(_inventory.GetAllSlots()[17], medKitBigInfo, 10);
        //_inventory.TryToAddIntoSlot(_inventory.GetAllSlots()[18], medKitBigInfo, 10);
    }

    private void OnEnable()
    {
        _inventory.OnInventoryStateChanged += RefreshSlots;
        _playerInputs.onTab += OnTab;
    }

    private void OnDisable()
    {
        _inventory.OnInventoryStateChanged -= RefreshSlots;
        _playerInputs.onTab -= OnTab;
    }

    private void OnTab()
    {
        if(_inventoryIsOpen)
            CloseInventory();
        else
            OpenInventory();
    }

    private void OpenInventory()
    {
        _inventoryIsOpen = true;

        Cursor.lockState = CursorLockMode.None;
        _playerInput.SwitchCurrentActionMap("Inventory");

        canvas.enabled = true;
    }

    private void CloseInventory()
    {
        _inventoryIsOpen = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        _playerInput.SwitchCurrentActionMap("Player");

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