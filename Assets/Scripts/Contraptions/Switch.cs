using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Switch : MonoBehaviour {
    public Device connectedDevice;

    protected int activeCount;

    private Animator animator;

	void Start ()
    {
        activeCount = 0;
        animator = GetComponent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activeCount++;
            if(connectedDevice)
                connectedDevice.sources++;
            TriggerDevice();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            activeCount--;
            if (connectedDevice)
                connectedDevice.sources--;
            TriggerDevice();
        }
    }

    void TriggerDevice()
    {
        bool active = activeCount != 0;
        animator.SetBool("active", active);
        if (connectedDevice)
        {
            if (active)
            {
                connectedDevice.Activate(this);
            }
            else
            {
                connectedDevice.Deactivate(this);
            }
        }
    }
}
