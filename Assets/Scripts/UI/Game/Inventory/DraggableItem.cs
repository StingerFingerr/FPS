using System;
using TMPro;
using UI.Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableItem: MonoBehaviour, 
    IBeginDragHandler, IEndDragHandler, IDragHandler, 
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private DraggableItemInfoPanel infoPanel;

    public UISlot UIInventorySlot { get; private set; }
    public InventoryItemInfo ItemInfo { get; set; }

    public static Action<bool> OnDragging;
    
    private IInventoryIcons _inventoryIcons;
    
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private bool _lockInfoPanel;

    [Inject]
    private void Construct(IInventoryIcons inventoryIcons) => 
        _inventoryIcons = inventoryIcons;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        UIInventorySlot = GetComponentInParent<UISlot>();
    }

    private void OnEnable() => 
        OnDragging += LockInfoPanel;

    private void OnDisable() => 
        OnDragging -= LockInfoPanel;

    public void Hide()
    {
        ItemInfo = null;
        gameObject.SetActive(false);
    }

    public void SetNewInfo(InventoryItemInfo itemInfo, int amount)
    {
        ItemInfo = itemInfo;
        icon.sprite = _inventoryIcons.GetIcon(itemInfo);
        if (amount > 1)
        {
            amountText.text = $"x{amount}";
            amountText.enabled = true;
        }
        else
        {
            amountText.enabled = false;
        }

        gameObject.SetActive(true);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UIInventorySlot.transform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
        OnDragging?.Invoke(true);
        infoPanel.Close();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        OnDragging?.Invoke(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_lockInfoPanel)
            return;
        
        if(ItemInfo is not  null)
        {
            UIInventorySlot.transform.SetAsLastSibling();
            infoPanel.Open(ItemInfo);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.Close();
    }

    private void LockInfoPanel(bool locked) => 
        _lockInfoPanel = locked;
}