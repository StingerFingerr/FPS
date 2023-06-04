using DG.Tweening;
using I2.Loc;
using UnityEngine;

public class DraggableItemInfoPanel: MonoBehaviour
{
    [SerializeField] private Localize titleLocalize;
    [SerializeField] private Localize descriptionLocalize;
        
    public void Open(InventoryItemInfo info)
    {
        titleLocalize.SetTerm(info.titleTerm);
        //titleLocalize.mTerm = info.titleTerm;
        descriptionLocalize.SetTerm(info.descriptionTerm);
        //descriptionLocalize.mTerm = info.descriptionTerm;
            
        transform.DOScale(1, .1f).SetEase(Ease.OutBack);
    }

    public void Close() => 
        transform.DOScale(0, .1f);
}