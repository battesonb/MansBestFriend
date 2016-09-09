using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Switch : MonoBehaviour {
    public Device connectedDevice;

    protected bool active;

    private Animator animator;

	void Start ()
    {
        active = false;
        animator = GetComponent<Animator>();
	}

    void onTriggerEnter()
    {
        active = true;
    }

    void onTriggerExit()
    {
        active = false;
    }

    void TriggerDevice()
    {
        animator.SetBool("active", active);
        if(active)
        {
            connectedDevice.Activate(this);
        } else
        {
            connectedDevice.Deactivate(this);
        }
    }
}
