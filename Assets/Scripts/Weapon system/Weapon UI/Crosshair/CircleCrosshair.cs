using DG.Tweening;
using UnityEngine;

public class CircleCrosshair: DynamicCrosshairBase
{
    
    private RectTransform _rectTransform;

    private void Awake() => 
        _rectTransform = GetComponent<RectTransform>();

    public override void Reset() => 
        _rectTransform.localPosition = Vector3.zero;

    public override void Activate()
    {
        _rectTransform.DOScale(1, .2f).SetEase(Ease.OutBack);
        gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        _rectTransform.DOScale(0, .2f);
        gameObject.SetActive(false);
    }

    public override void OnShot() => 
        _rectTransform.DOPunchScale(Vector3.one * .5f, .3f, 0).SetEase(Ease.OutCubic);

    public override void OnAim(bool isAiming)
    {
        if(isAiming)
            Deactivate();
        else
            Activate();
    }

    public override void OnMove(bool isMoving)
    {
        
    }

    public override void OnLook(bool isLooking)
    {
        
    }
}