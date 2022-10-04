using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Contraption
{
    [SerializeField] private Animator animator;
    public bool Activate(){
        animator.SetTrigger("OpenDoor");
        return true;
    }

    public bool Deactivate(){
        animator.SetTrigger("CloseDoor");
        return true;
    }
}
