using UnityEngine;

namespace Enemy
{
    public class TestEnemy: MonoBehaviour, IDamageable
    {
        private int _health = 10000;
        
        public void SetDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)            
                Destroy(gameObject);
        }
    }
}