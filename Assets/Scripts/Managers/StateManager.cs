using UnityEngine;
using System.Collections;

public class StateManager
{
    private static StateManager instance;
    public static StateManager Instance
    {
        get {
            if (instance == null)
                instance = new StateManager();
            return instance;
        }
    }

    public bool humanActive = true;
    public int level = 1;
}
