using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Human humanPrefab;
    public Dog dogPrefab;

    private Camera mainCamera;

    [HideInInspector]
    public Human human;
    [HideInInspector]
    public Dog dog;

	void Start ()
    {
        mainCamera = GetComponent<Camera>();

        instance = this;

        if (StateManager.Instance.level != 0)
        {
            if(dogPrefab)
                dog = Instantiate(dogPrefab);
            if(humanPrefab)
                human = Instantiate(humanPrefab);
        }
    }

    void Update()
    {
        if (StateManager.Instance.level != 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StateManager.Instance.humanActive = !StateManager.Instance.humanActive;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel("level" + StateManager.Instance.level);
            }

            CameraFollowActive();
        }
    }

    void CameraFollowActive()
    {
        if(StateManager.Instance.humanActive && human)
        {
            Vector3 newCamPos = new Vector3(human.transform.position.x, human.transform.position.y, -10);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPos, 10f * Time.deltaTime);
        }
        else if(dog)
        {
            Vector3 newCamPos = new Vector3(dog.transform.position.x, dog.transform.position.y, -10);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPos, 10f * Time.deltaTime);
        }
    }

    // More StateManager-esque methods, but we need Coroutines from MonoBehaviour
    public void loadNextLevel(bool incLevel = true)
    {
        loadScreenThenLevel(incLevel);
    }

    private void loadScreenThenLevel(bool incLevel)
    {
        if(incLevel)
            StateManager.Instance.level++;
        if (StateManager.Instance.level < Application.levelCount) // Accounts for menu screen
        {
            Application.LoadLevel("Level" + StateManager.Instance.level);

            StateManager.Instance.humanActive = true;
        }
        else
        {
            Application.LoadLevel("Menu");
            StateManager.Instance.level = 0;
        }
    }
}
