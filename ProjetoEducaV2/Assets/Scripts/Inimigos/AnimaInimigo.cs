using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AnimaInimigo 
{
    public static void ChangeAnimState(Animator animator, string state)
    {
        if (animator != null)
        {
            animator.Play(state);
        }
        else
        {
            Debug.LogWarning("Animator is null");
        }
    }
}
/*public class AnimaInimigo
{
    public static AnimatorStateInfo ChangeAnimState(Animator animator, string state)
    {
        if (state == "Attack")
            Debug.LogWarning("attack");
        if (animator != null)
        {
            animator.Play(state);
            return animator.GetCurrentAnimatorStateInfo(0);
        }
        else
        {
            Debug.LogWarning("Animator is null");
            return default(AnimatorStateInfo);
        }
        
    }
}*/

