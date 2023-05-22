using System;
using UnityEngine;
using Zenject;

public class BaseBossZombie: BaseZombie
{
    public new class Factory: PlaceholderFactory<Vector3, Action, BaseBossZombie>
    { }
    public new class Pool: MonoPoolableMemoryPool<Vector3, Action, IMemoryPool, BaseBossZombie>
    { }
    
}