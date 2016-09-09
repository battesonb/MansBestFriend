using UnityEngine;
using System.Collections;

public class Human : Mover {
    Animator animator;

    protected override void Start ()
    {
        base.Start();

        animator = GetComponent<Animator>();
	}

    protected override void Update ()
    {
        base.Update();

        animator.SetFloat("speed", Mathf.Abs(currentSpeed));
	}
}
