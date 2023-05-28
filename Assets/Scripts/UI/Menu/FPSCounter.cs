using System.Collections;
using TMPro;
using UnityEngine;

public class FPSCounter: BaseWindow
{
    [SerializeField] private TextMeshProUGUI fpsText;

    private int _framesAmount;

    public override void Open()
    {
        gameObject.SetActive(true);
        ResetFrames();
        StartCoroutine(CountFrames());
        StartCoroutine(CalculateFPS());
    }

    public override void Close() => 
        gameObject.SetActive(false);

    private IEnumerator CountFrames()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _framesAmount++;
        }
    }

    private IEnumerator CalculateFPS()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            fpsText.text = $"fps: {_framesAmount}";
            ResetFrames();
        }
    }

    private void ResetFrames() => 
        _framesAmount = 0;
}