using UnityEngine;

public class BaseWindow: MonoBehaviour
{
    public virtual bool IsOpened => gameObject.activeSelf;
    
    public virtual void Open() => 
        gameObject.SetActive(true);

    public virtual void Close() => 
        gameObject.SetActive(false);
}