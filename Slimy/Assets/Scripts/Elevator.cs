using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, Contraption
{
    [SerializeField] private Animator animator;
    public bool Activate()
    {
        animator.SetTrigger("Activate");
        return true;
    }

    public bool Deactivate()
    {
        animator.SetTrigger("Deactivate");
        return true;
    }
}
