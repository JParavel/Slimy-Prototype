using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, Contraption
{
    [SerializeField] private Animator animator;
    public float animationSpeed;
    public bool Activate()
    {
        animator.SetFloat("Speed", animationSpeed);
        animator.Play("ElevatorAnimation", 0, GetTime());
        return true;
    }

    public bool Deactivate()
    {
        animator.SetFloat("Speed", -animationSpeed);
        animator.Play("ElevatorAnimation", 0, GetTime());
        return true;
    }

    private float GetTime()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ElevatorAnimation"))
        {
            float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            return Mathf.Clamp(time, 0f, 1f);
        }

        return 0;
    }
}
