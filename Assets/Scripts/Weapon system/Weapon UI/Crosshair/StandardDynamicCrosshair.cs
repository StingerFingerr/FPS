using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StandardDynamicCrosshair: DynamicCrosshairBase
{
    public List<Image> crosshairElements;
    
    private RectTransform _rectTransform;

    private float _targetSize;
    private float _currentSize;
    private bool _isVisible;
    private Color _color;
    private float _alfa;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        _color = crosshairElements.First().color;
    }

    public override void Reset()
    {
        _rectTransform.localPosition = Vector3.zero;
        _currentSize = normalSize;
        _targetSize = maxSizeOnShot;
        _alfa = 0;
    }

    private void Update()
    {
        _currentSize = Mathf.Lerp(_currentSize, _targetSize, Time.deltaTime * shootingSpeed);
        
        if(_isVisible)
        {
            _targetSize = Mathf.Lerp(_targetSize, normalSize, Time.deltaTime * restingSpeed);
            _alfa = Mathf.Lerp(_alfa, 1, Time.deltaTime * meltingSpeed);
        }
        else
        {
            _alfa = Mathf.Lerp(_alfa, 0, Time.deltaTime * meltingSpeed);
        }

        _color.a = _alfa;
        crosshairElements.ForEach(e=> e.color = _color);
        _rectTransform.sizeDelta = new Vector2(_currentSize,_currentSize);
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        Show();
    }

    public override void Deactivate() => 
        gameObject.SetActive(false);

    public override void OnAim(bool isAiming)
    {
        if(isAiming)
            Hide();
        else 
            Show();
    }

    public override void OnShot()
    {
        if(_isVisible is false)
            return;
        
        if (_targetSize < maxSizeOnShot)
            _targetSize = maxSizeOnShot;
    }

    public override void OnMove(bool isMoving)
    {
        if(_isVisible is false)
            return;
        
        if (isMoving)
            if(_targetSize<maxSizeOnMove)
                _targetSize = maxSizeOnMove;
    }

    public override void OnLook(bool isLooking)
    {
        if(_isVisible is false)
            return;
        
        if (isLooking)
            if(_targetSize<maxSizeOnLook)
                _targetSize = maxSizeOnLook;
    }

    private void Show()
    {
        _isVisible = true;
    }

    private void Hide()
    {
        _isVisible = false;

        _targetSize = hiddenSize;
    }

}