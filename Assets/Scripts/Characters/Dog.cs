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

        bool active = !StateManager.Instance.humanActive;
        Move(active);

        animator.SetFloat("speed", Mathf.Abs(currentSpeed.x));
        animator.SetBool("grounded", grounded);
        animator.SetBool("active", active);
    }
}
