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

    public IEnumerator loadLevelAsync(int levelNo)
    {
        AsyncOperation async = Application.LoadLevelAsync("level" + levelNo);
        yield return async;
        humanActive = true;
    }

    public IEnumerator loadLevelAsync()
    {
        if (level <= Application.levelCount)
        {
            AsyncOperation async = Application.LoadLevelAsync("level" + level);
            yield return async;
            humanActive = true;
        }
        else
        {
            AsyncOperation async = Application.LoadLevelAsync("menu");
            yield return async;
        }
    }
}
