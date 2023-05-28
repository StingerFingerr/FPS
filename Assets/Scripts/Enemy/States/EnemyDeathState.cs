using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathState: StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<NavMeshAgent>().isStopped = true;
        animator.GetComponent<BaseZombie>().enabled = false;
    }
}