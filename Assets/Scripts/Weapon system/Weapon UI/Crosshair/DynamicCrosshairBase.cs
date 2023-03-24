using UnityEngine;
using Zenject;

public abstract class DynamicCrosshairBase: MonoBehaviour
{
    public class CachedFactory: PlaceholderFactory<CrosshairType ,DynamicCrosshairBase> { }
    
    public CrosshairType type;
    
    public float normalSize;
    public float hiddenSize;
    public float maxSizeOnShot;
    public float maxSizeOnMove;
    public float maxSizeOnLook;

    public float restingSpeed;
    public float shootingSpeed;
    public float meltingSpeed;
    
    public abstract void Reset();
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void OnShot();

    public abstract void OnAim(bool isAiming);
    public abstract void OnMove(bool isMoving);
    public abstract void OnLook(bool isLooking);
}
