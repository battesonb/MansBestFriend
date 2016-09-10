using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour {
    private Button playButton;

	void Start ()
    {
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(() => {
            StartCoroutine(StateManager.Instance.loadLevelAsync());
        });
	}
}
