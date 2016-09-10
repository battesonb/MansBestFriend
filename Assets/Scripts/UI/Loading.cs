using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Loading : MonoBehaviour {
    Text levelText;
    Image levelImageBG;
    
	void Start ()
    {
        GameObject textGO = GameObject.FindGameObjectWithTag("levelText");
        levelText = textGO.GetComponent<Text>();
        levelText.text = "Level " + StateManager.Instance.level;
        levelImageBG = GetComponentInChildren<Image>();

        levelImageBG.CrossFadeAlpha(0, 1.5f, true);
        levelText.CrossFadeAlpha(0, 1.5f, true);
    }

}
