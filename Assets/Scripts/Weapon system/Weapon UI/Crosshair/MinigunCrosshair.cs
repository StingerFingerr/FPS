using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MinigunCrosshair: DynamicCrosshairBase
{
    public List<Image> normalImages;
    public List<Image> aimingImages;

    private RectTransform _rectTransform;

    private bool _isAiming;
    private float _currentSize;
    private float _targetSize;

    private float _alfaForNormals;
    private float _alfaForAiming;

    private Color _normalColor;
    private Color _aimingColor;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _normalColor = normalImages.First().color;
        _aimingColor = aimingImages.First().color;
    }

    public override void Reset()
    {
        _rectTransform.localPosition = Vector3.zero;
        _currentSize = normalSize;
        _targetSize = maxSizeOnShot;

        _alfaForNormals = 1;
        _alfaForAiming = 0;
    }

    private void Update()
    {
        _currentSize = Mathf.Lerp(_currentSize, _targetSize, Time.deltaTime * shootingSpeed);
        
        if (_isAiming)
        {
            _alfaForAiming = Mathf.Lerp(_alfaForAiming, 1, Time.deltaTime * meltingSpeed);
            _alfaForNormals = Mathf.Lerp(_alfaForNormals, 0, Time.deltaTime * meltingSpeed);
        }
        else
        {
            _targetSize = Mathf.Lerp(_targetSize, normalSize, Time.deltaTime * restingSpeed);
            
            _alfaForAiming = Mathf.Lerp(_alfaForAiming, 0, Time.deltaTime * meltingSpeed);
            _alfaForNormals = Mathf.Lerp(_alfaForNormals, 1, Time.deltaTime * meltingSpeed);
        }

        _normalColor.a = _alfaForNormals;
        _aimingColor.a = _alfaForAiming;
        
        normalImages.ForEach(i=> i.color = _normalColor);
        aimingImages.ForEach(i=> i.color = _aimingColor);
        
        _rectTransform.sizeDelta = new Vector2(_currentSize,_currentSize);
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        Show();
    }

    public override void Deactivate() => 
        gameObject.SetActive(false);

    public override void OnShot()
    {
        if(_isAiming)
            return;
        
        if (_targetSize < maxSizeOnShot)
            _targetSize = maxSizeOnShot;
    }

    public override void OnAim(bool isAiming)
    {
        _isAiming = isAiming;
        if (isAiming)
        {
            _targetSize = 0;
        }
        else
        {
            _targetSize = normalSize;
        }
    }

    public override void OnMove(bool isMoving)
    {
        if(_isAiming)
            return;

        if (isMoving)
            if(_targetSize<maxSizeOnMove)
                _targetSize = maxSizeOnMove;
    }

    public override void OnLook(bool isLooking)
    {
        if(_isAiming)
            return;
        
        if (isLooking)
            if(_targetSize<maxSizeOnLook)
                _targetSize = maxSizeOnLook;
    }

    private void Show()
    {
        _targetSize = maxSizeOnShot;
    }
}