using UnityEngine;
using System.Collections;

public abstract class Device : MonoBehaviour
{
    [HideInInspector]
    public int sources = 0;

    public abstract void Activate(Component caller);

    public abstract void Deactivate(Component caller);
}
