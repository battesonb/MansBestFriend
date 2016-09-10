using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    [HideInInspector]
    public static AudioManager instance;

    public AudioClip music;

	void Start ()
    {
        if(instance && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.clip = music;
        audio.Play();
    }

	void Update ()
    {
	    
	}
}
