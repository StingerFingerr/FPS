using System;
using UnityEngine;
using Zenject;

public class BaseBossZombie: BaseZombie
{
    public new class Factory: PlaceholderFactory<Vector3, Action, BaseBossZombie>
    { }

    public new class Pool : MonoPoolableMemoryPool<Vector3, Action, IMemoryPool, BaseBossZombie>
    {
        protected override void Reinitialize(Vector3 p1, Action p2, IMemoryPool p3, BaseBossZombie item)
        {
            item.transform.position = p1;
            base.Reinitialize(p1, p2, p3, item);
        }
    }
    
}