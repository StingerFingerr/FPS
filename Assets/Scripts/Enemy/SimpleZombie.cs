using UnityEngine;
using UnityEngine.AI;

public class SimpleZombie: BaseZombie
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemySettings settings;
    
    private static readonly int IsChasing = Animator.StringToHash("IsChasing");
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    private void Start()
    {
        agent.stoppingDistance = settings.stoppingDistance;
    }

    private void Update()
    {
        if (Player is null)
            return;

        bool inClose = InClose();
        animator.SetBool(IsAttacking, inClose);
        if (inClose)        
            transform.LookAt(Player);
    }
    
    private bool InClose() => 
        Vector3.Distance(agent.transform.position, Player.position) < agent.stoppingDistance;

    private void OnTriggerEnter(Collider other)
    {
        Player = other.transform;
        animator.SetBool(IsChasing, true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool(IsChasing, false);
    }
}