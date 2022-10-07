using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Animator animator;
    private bool open;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            float time = 0;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("TestAnimation"))
            {
                time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            }
            

            if (open)
            {
                open = false;
                animator.SetFloat("Speed", -0.5f);
                if (time > 1)
                {
                    time = 1;
                }
            }
            else
            {
                open = true;
                animator.SetFloat("Speed", 0.5f);
                if (time < 0)
                {
                    time = 0;
                }

            }

            animator.Play("TestAnimation", 0, time);


        }
    }
}
