using System.Collections;
using Effects;
using UnityEngine;
using Zenject;

namespace Shooting
{
    public class Bullet: MonoBehaviour, IPoolable<Vector3, Vector3, int, IMemoryPool>
    {
        public class Factory : PlaceholderFactory<Vector3, Vector3, int, Bullet>
        { }
        public class Pool : MonoPoolableMemoryPool<Vector3, Vector3, int, IMemoryPool, Bullet>
        { }
        
        [SerializeField] private LayerMask mask;
        [SerializeField] private TrailRenderer trail;

        private IMemoryPool _pool;
        
        private BulletImpact.Factory _bulletsImpactsFactory;
        private BloodyBulletImpact.Factory _bloodyBulletsImpactsFactory;

        [Inject]
        private void Construct(BulletImpact.Factory bulletsImpactsFactory,
            BloodyBulletImpact.Factory bloodyBulletsImpactsFactory)
        {
            _bulletsImpactsFactory = bulletsImpactsFactory;
            _bloodyBulletsImpactsFactory = bloodyBulletsImpactsFactory;
        }

        public void OnSpawned(Vector3 startPos, Vector3 destination, int damage, IMemoryPool pool)
        {
            _pool = pool;

            if (Physics.Raycast(startPos, destination - startPos, out RaycastHit hit, 100, mask))
            {
                var damageable = hit.transform.GetComponent<IDamageable>();
                
                if(damageable is not null)
                {
                    damageable.SetDamage(damage);
                    _bloodyBulletsImpactsFactory.Create(hit.point, hit.normal);
                }
                else
                {
                    _bulletsImpactsFactory.Create(hit.point, hit.normal);
                }
            }

            StartCoroutine(MoveTrail(startPos, destination));
        }

        private IEnumerator MoveTrail(Vector3 startPos, Vector3 destination)
        {
            float time = .02f;

            if (Vector3.Distance(startPos, destination) < 1)
                time = 1;
            
            while (time < 1)
            {
                transform.position = Vector3.Lerp(startPos, destination, time);
                time += Time.deltaTime / trail.time;
                yield return null;
            }
            
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            trail.Clear();
            _pool = null;
        }
    }
}