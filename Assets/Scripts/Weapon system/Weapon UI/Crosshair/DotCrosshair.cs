using UnityEngine;

public class DotCrosshair: DynamicCrosshairBase
{
    private RectTransform _rectTransform;

    private void Awake() => 
        _rectTransform = GetComponent<RectTransform>();

    public override void Reset() => 
        _rectTransform.localPosition = Vector3.zero;

    public override void Activate() => 
        gameObject.SetActive(true);

    public override void Deactivate() => 
        gameObject.SetActive(false);

    public override void OnShot()
    {
        
    }

    public override void OnAim(bool isAiming)
    {
        
    }

    public override void OnMove(bool isMoving)
    {
        
    }

    public override void OnLook(bool isLooking)
    {
        
    }
}