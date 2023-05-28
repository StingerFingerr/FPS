using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackingState: StateMachineBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _target ??= animator.GetComponent<BaseZombie>().Player;
        _agent ??= animator.GetComponent<NavMeshAgent>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}