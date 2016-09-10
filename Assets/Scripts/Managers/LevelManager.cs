using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public Text talkText;
    public BoxCollider2D finish;
    public Hint[] hints;

    private bool loading = false;

    [Serializable]
    public class Hint
    {
        public BoxCollider2D collider;
        public string text;
    }
    
    void Start ()
    {
        loading = false;
    }

	void Update ()
    {
        if (StateManager.Instance.level != 0)
        {
            UpdateHints();

            CheckIfFinished();
        }
    }

    void UpdateHints()
    {
        if (GameManager.instance.human && GameManager.instance.human)
        {
            Collider2D humanCollider = GameManager.instance.human.GetComponent<BoxCollider2D>();
            Collider2D dogCollider = GameManager.instance.dog.GetComponent<BoxCollider2D>();
            if (humanCollider && dogCollider)
            {
                foreach (Hint hint in hints)
                {
                    bool touching = hint.collider.IsTouching(humanCollider) || hint.collider.IsTouching(dogCollider);

                    if (touching)
                    {
                        talkText.text = hint.text;
                        break;
                    }
                }
            }
        }
    }

    void CheckIfFinished()
    {
        if (GameManager.instance.human && GameManager.instance.human)
        {
            Collider2D humanCollider = GameManager.instance.human.GetComponent<Collider2D>();
            Collider2D dogCollider = GameManager.instance.dog.GetComponent<Collider2D>();

            bool touching = finish.IsTouching(humanCollider) && finish.IsTouching(dogCollider);

            if (touching && !loading)
            {
                loading = true;
                GameManager.instance.loadNextLevel();
            }
        }
    }
}
