using UnityEngine;

public class WaitForLoad : StateMachineBehaviour
{ 
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("StartFade", false);
        animator.SetBool("SelectedPerk", false);
    }
    
}
