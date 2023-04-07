using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI.Game.Inventory
{
    public abstract class UISlot: MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public DraggableItem draggableItem;

        public Image slotBack;
        public Color normalColor;
        public Color highlightedColor;

        protected IInventory Inventory;
        
        [Inject]
        private void Construct(IInventory inventory) => 
            Inventory = inventory;

        public abstract void OnDrop(PointerEventData eventData);
        public abstract void Drop();
        public virtual void Clear(){}

        public void OnPointerEnter(PointerEventData eventData) => 
            slotBack.DOColor(highlightedColor, .1f);

        public void OnPointerExit(PointerEventData eventData) => 
            slotBack.DOColor(normalColor, .1f);

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)            
                Drop();

        }
    }
}