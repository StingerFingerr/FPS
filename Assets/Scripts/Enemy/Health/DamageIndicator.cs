using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class DamageIndicator: MonoBehaviour, IPoolable<Vector3, Vector3, int, IMemoryPool>
{
    public TextMeshProUGUI damageText;

    public float duration = 1f;

    private IMemoryPool _pool;
    
    public class Factory: PlaceholderFactory<Vector3, Vector3, int, DamageIndicator>
    { }

    public class Pool : MonoPoolableMemoryPool<Vector3, Vector3, int, IMemoryPool, DamageIndicator>
    { }


    public void OnSpawned(Vector3 pos, Vector3 forward, int damage, IMemoryPool pool)
    {
        transform.position = pos + Vector3.up*.5f;
        transform.forward = forward;
        transform.localScale = Vector3.zero;
        
        damageText.text = $"{damage}";
        _pool = pool;
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        var offset = new Vector3(
            Random.Range(-.5f, .5f), 
            Random.Range(.3f, .5f),
            0);

        var targetPos = transform.position + offset;


        transform
            .DOMoveX(targetPos.x, duration * 1f)
            .SetEase(Ease.OutCubic);
        
        transform
            .DOMoveY(targetPos.y, duration * 1f)
            .SetEase(Ease.OutQuad);

        transform
            .DOScale(1, duration / 2)
            .SetEase(Ease.OutExpo)
            .OnKill(() =>
            {
                transform
                    .DOScale(0, duration / 2)
                    .SetEase(Ease.InExpo)
                    .OnKill(()=> _pool.Despawn(this));
            });
    }

    public void OnDespawned()
    {
        
    }
}