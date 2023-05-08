using UnityEngine;
using UnityEngine.AI;

public class EnemyChasingState: StateMachineBehaviour
{
    [SerializeField] private EnemySettings settings;
    
    private NavMeshAgent _agent;
    private Transform _target;

    private float _chasingTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent ??= animator.GetComponent<NavMeshAgent>();
        _target ??= animator.GetComponent<BaseZombie>().Player;

        _agent.speed = settings.chasingSpeed;
        _agent.stoppingDistance = settings.stoppingDistance;
        
        SetNewDestination();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_agent.isStopped)
            return;
        
        _chasingTime += Time.deltaTime;
        
        if (_chasingTime >= settings.checkDestinationTime)
        {
            CheckDestination();
            _chasingTime = 0;
        }
    }

    private void CheckDestination()
    {
        if (Vector3.Distance(_target.position, _agent.transform.position) > _agent.stoppingDistance)
            SetNewDestination();
    }

    private void SetNewDestination()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_target.position);
    }
}