using UnityEngine;
using System.Collections;
using System;

public class Reveal : Device {
    private Behaviour behaviour;
    private Renderer[] renderers;

    void Start()
    {
        behaviour = GetComponent<Behaviour>();
        behaviour.enabled = false; // In case the user hasn't done so, yet.

        renderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    public override void Activate(Component caller)
    {
        behaviour.enabled = true;
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }

    public override void Deactivate(Component caller)
    {
        // No deactive, this is just for revealing.
    }
}
