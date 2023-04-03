using TMPro;
using UI.Game.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableItem: MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;

    public UISlot UIInventorySlot { get; private set; }
    public InventoryItemInfo ItemInfo { get; set; }

    private IInventoryIcons _inventoryIcons;
    
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    [Inject]
    private void Construct(IInventoryIcons inventoryIcons) => 
        _inventoryIcons = inventoryIcons;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        UIInventorySlot = GetComponentInParent<UISlot>();
    }

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
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
}