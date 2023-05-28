using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Toggle))]
public class FPSCounterManager: MonoBehaviour
{
    private FPSCounter _fpsCounter;
    private Toggle _toggle;

    [Inject]
    private void Construct(FPSCounter fpsCounter) => 
        _fpsCounter = fpsCounter;

    private void Awake() => 
        _toggle = GetComponent<Toggle>();

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SwitchCounter);
        UpdateToggleView();
    }

    private void OnDisable() => 
        _toggle.onValueChanged.RemoveListener(SwitchCounter);

    private void UpdateToggleView() => 
        _toggle.isOn = _fpsCounter.IsOpened;

    private void SwitchCounter(bool isOn)
    {
        if(isOn)
            _fpsCounter.Open();
        else
            _fpsCounter.Close();
    }
}