using System;
using System.Threading.Tasks;
using MoreMountains.Tools;
using Player.Collectable_items;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class BaseZombie: MonoBehaviour, IPoolable<Vector3, Action, IMemoryPool>
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected EnemyHealth health;
    [SerializeField] protected EnemySettings settings;
    [SerializeField] private Target targetIndicator;
    [SerializeField] protected LayerMask playerMask;

    [SerializeField] private CollectableItemDropper itemDropper;
    [SerializeField] private DropSettings dropSettings;

    private PlayerHealth _playerHealth;

    public class Factory: PlaceholderFactory<Vector3, Action, BaseZombie>
    { }
    public class Pool : MonoPoolableMemoryPool<Vector3, Action, IMemoryPool, BaseZombie>
    {
        protected override void Reinitialize(Vector3 p1, Action p2, IMemoryPool p3, BaseZombie item)
        {
            item.transform.position = p1;
            base.Reinitialize(p1, p2, p3, item);
        }
    }
    
    public Transform Player { get; protected set; }

    private static readonly int IsChasing = Animator.StringToHash("IsChasing");
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    
    private IMemoryPool _pool;
    private Action _onKilled;

    public async void Kill()
    {
        _onKilled?.Invoke();
        _onKilled = null;
        targetIndicator.enabled = false;
        DropItem();
        await Task.Delay(5000);
        _pool.Despawn(this);
    }

    protected virtual void DropItem()
    {
        var info = dropSettings.GetItemInfo();
        if(info is not null)
            itemDropper.DropItem(info, info.maxItemsInSlot);
    }


    public void OnSpawned(Vector3 pos, Action onKill, IMemoryPool pool)
    {
        _pool = pool;
        _onKilled = onKill;

        transform.position = pos;

        targetIndicator.enabled = true;
        enabled = true;
        health.Reset();
    }

    private void Start() => 
        agent.stoppingDistance = settings.stoppingDistance;

    private void Update()
    {
        if (Player is null)
            return;

        bool inClose = InClose();
        animator.SetBool(IsAttacking, inClose);
        if (inClose)        
            transform.LookAt(Player);
    }

    public virtual void Attack()
    {
        if (Vector3.Distance(Player.position, transform.position) < settings.attackDistance)
        {
            _playerHealth.SetDamage(settings.damage,Vector3.zero);
        }
    }
    
    protected virtual void OnStartChasing() => 
        animator.SetBool(IsChasing, true);

    protected virtual void CancelChasing() => 
        animator.SetBool(IsChasing, false);

    protected bool InClose() => 
        Vector3.Distance(agent.transform.position, Player.position) < agent.stoppingDistance;

    private void OnTriggerExit(Collider other) => 
        CancelChasing();

    private void OnTriggerEnter(Collider other)
    {
        Player ??= other.transform;
        _playerHealth ??= other.GetComponent<PlayerHealth>();
        OnStartChasing();
    }

    public void OnDespawned()
    { }
}