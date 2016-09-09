using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Dog : Mover
{
    protected Animator animator;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        animator.SetFloat("speed", Mathf.Abs(currentSpeed.x));
        if (jumped)
        {
            animator.SetTrigger("jumped");
            jumped = false;
        }
        animator.SetBool("grounded", grounded);
    }
}
