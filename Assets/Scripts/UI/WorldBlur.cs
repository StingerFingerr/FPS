using UnityEngine;

public class WorldBlur: MonoBehaviour
{
    public void Show() => 
        gameObject.SetActive(true);

    public void Hide() => 
        gameObject.SetActive(false);
}