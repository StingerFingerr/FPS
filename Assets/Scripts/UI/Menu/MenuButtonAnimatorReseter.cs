using UnityEngine;

public class MenuButtonAnimatorReseter: MonoBehaviour
{
    [SerializeField] private string triggerOnEnable;
    [SerializeField] private Animator animator;
    
    private void OnEnable() => 
        animator.SetTrigger(triggerOnEnable);
}