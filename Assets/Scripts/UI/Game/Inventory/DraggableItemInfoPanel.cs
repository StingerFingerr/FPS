using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Game.Inventory
{
    public class DraggableItemInfoPanel: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
      
        public void Open(InventoryItemInfo info)
        {
            title.text = info.title;
            description.text = info.description;

            transform.DOScale(1, .1f).SetEase(Ease.OutBack);
        }

        public void Close()
        {
            transform.DOScale(0, .1f);
        }
    }
}