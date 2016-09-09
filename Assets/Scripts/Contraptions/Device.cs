using UnityEngine;
using System.Collections;

public abstract class Device : MonoBehaviour
{
    public abstract void Activate(Component caller);

    public abstract void Deactivate(Component caller);
}
