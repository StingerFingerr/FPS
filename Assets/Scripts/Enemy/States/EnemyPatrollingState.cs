using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollingState: StateMachineBehaviour
{
    [SerializeField] private EnemySettings settings;
    
    private NavMeshAgent _agent;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent ??= animator.GetComponent<NavMeshAgent>();

        _agent.speed = settings.patrollingSpeed;
        _agent.stoppingDistance = 0;
        
        SetNewDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_agent.remainingDistance < .1f)
            SetNewDestination();
    }


    private void SetNewDestination()
    {
        Vector3 offset = new Vector3(
            Random.Range(-settings.patrollingRadius, settings.patrollingRadius), 
            0,
            Random.Range(-settings.patrollingRadius, settings.patrollingRadius));
        _agent.SetDestination(_agent.transform.position + offset);
    }
}