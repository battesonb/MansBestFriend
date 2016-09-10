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
    public int level = 0;

    public IEnumerator loadLevelAsync()
    {
        level++;
        AsyncOperation async = Application.LoadLevelAsync("Level" + level);
        yield return async;
        humanActive = true;
    }

    public void loadNextLevel()
    {
        level++;
        if (level < Application.levelCount)
        {
            Application.LoadLevel("Level" + level);

            humanActive = true;
        }
        else
        {
            Application.LoadLevel("Menu");
            level = 0;
        }
    }
}
