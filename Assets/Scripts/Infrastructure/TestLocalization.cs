using System.Collections;
using I2.Loc;
using UnityEngine;

public class TestLocalization: MonoBehaviour
{
    private void Start()
    {
        Debug.Log(I2.Loc.ScriptLocalization.Term2);
        

        LocalizationManager.CurrentLanguage = "English";
        
        Debug.Log(I2.Loc.ScriptLocalization.Term2);

        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(2);
        
        LocalizationManager.CurrentLanguage = "Belarusian";
        
        Debug.Log(I2.Loc.ScriptLocalization.Term2);
    }
}