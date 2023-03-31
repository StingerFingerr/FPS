using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UIInventorySlot: MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public DraggableItem draggableItem;

    public Image slotBack;
    public Color normalColor;
    public Color highlightedColor;

    private IInventory _inventory;
    public IInventorySlot InventorySlot { get; private set; }

    [Inject]
    private void Construct(IInventory inventory) => 
        _inventory = inventory;

    public void SetupInventorySlot(IInventorySlot slot) => 
        InventorySlot = slot;

    public void OnDrop(PointerEventData eventData)
    {
        var draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        draggableItem.OnEndDrag(null);
        
        _inventory.MoveItemFromSlotToSlot(draggableItem.UIInventorySlot.InventorySlot, InventorySlot);
    }

    public void Refresh()
    {
        if(InventorySlot.IsEmpty)
            draggableItem.Hide();
        else
            draggableItem.SetNewInfo(InventorySlot.ItemInfo, InventorySlot.Amount);
    }

    public void OnPointerEnter(PointerEventData eventData) => 
        slotBack.DOColor(highlightedColor, .1f);

    public void OnPointerExit(PointerEventData eventData) => 
        slotBack.DOColor(normalColor, .1f);
}