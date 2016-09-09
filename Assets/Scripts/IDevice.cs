using UnityEngine;
using System.Collections;

public interface IDevice {
    void Activate(Component caller);

    void Deactivate(Component caller);
}
