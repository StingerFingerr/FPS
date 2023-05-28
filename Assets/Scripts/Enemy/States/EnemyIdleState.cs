using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState: StateMachineBehaviour
{
    private NavMeshAgent _agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.isStopped = true;
    }
}