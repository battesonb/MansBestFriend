using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Human humanPrefab;
    public Dog dogPrefab;

    private Camera mainCamera;
    private Human human;
    private Dog dog;

	void Start ()
    {
        mainCamera = GetComponent<Camera>();

        instance = this;

        dog = Instantiate(dogPrefab);
        human = Instantiate(humanPrefab);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StateManager.Instance.humanActive = !StateManager.Instance.humanActive;
        }

        CameraFollowActive();
    }

    void CameraFollowActive()
    {
        if(StateManager.Instance.humanActive)
        {
            Vector3 newCamPos = new Vector3(human.transform.position.x, human.transform.position.y, -10);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPos, 10f * Time.deltaTime);
        } else
        {
            Vector3 newCamPos = new Vector3(dog.transform.position.x, dog.transform.position.y, -10);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCamPos, 10f * Time.deltaTime);
        }
    }
}
