using UnityEngine;
using System.Collections;
using System;

public class Hider : Device {
    public override void Activate(Component caller)
    {
        recursivelySetActive(transform, false);
    }

    public override void Deactivate(Component caller)
    {
        foreach (Transform child in transform)
        {
            setActive(child, true);
        }
    }

    private void setActive(Transform transform, bool state)
    {
        SpriteRenderer childSprite = transform.GetComponent<SpriteRenderer>();
        BoxCollider2D childCollider = transform.GetComponent<BoxCollider2D>();
        if(childSprite)
            childSprite.enabled = state;
        if(childCollider)
            childCollider.enabled = state;
    }

    private void recursivelySetActive(Transform transform, bool state)
    {
        foreach (Transform child in transform)
        {
            setActive(child, state);
            recursivelySetActive(child, state);
        }
    }
}
