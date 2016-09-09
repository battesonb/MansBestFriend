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
        animator.SetBool("grounded", grounded);
    }
}
