using UnityEngine;
using Zenject;

namespace Effects
{
    public class BloodyBulletImpact: MonoBehaviour, IPoolable<Vector3, Vector3, IMemoryPool>
    {
        public class Factory: PlaceholderFactory<Vector3, Vector3, BloodyBulletImpact>
        { }
        public class Pool: MonoPoolableMemoryPool<Vector3, Vector3, IMemoryPool, BloodyBulletImpact>
        { }
        
        private IMemoryPool _pool;
        private bool _isSpawned;
        
        public void OnSpawned(Vector3 pos, Vector3 normal, IMemoryPool pool)
        {
            _pool = pool;
            _isSpawned = true;

            transform.position = pos;
            transform.rotation = Quaternion.LookRotation(normal);
        }

        private void OnDisable()
        {
            if (_isSpawned)            
                _pool.Despawn(this);
        }

        public void OnDespawned() => 
            _pool = null;
    }
}