using System.Collections;
using System.Collections.Generic;
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
