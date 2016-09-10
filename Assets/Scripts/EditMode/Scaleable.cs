using UnityEngine;
using System.Collections;

// This class is not working, but I'd really like to work on it at some point.
[RequireComponent (typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class Scaleable : MonoBehaviour {
    SpriteRenderer sprite;
    Vector2 originalSize;

	void Start ()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

	void LateUpdate ()
    {
        if (Application.isEditor)
            Start();

        Vector2 size = sprite.bounds.size;
        originalSize = new Vector2(size.x / transform.localScale.x, size.y / transform.localScale.y);

        GameObject prefab = new GameObject();
        SpriteRenderer prefabSprite = prefab.AddComponent<SpriteRenderer>();
        prefab.transform.position = transform.position;
        prefabSprite.sprite = sprite.sprite;

        foreach(Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for(int x = 0; x <= Mathf.FloorToInt(size.x); x++)
        {
            Debug.Log("x: " + x);
            GameObject child = Instantiate(prefab);
            child.transform.position = transform.position + (new Vector3(originalSize.x * x, 0, 0));
            child.transform.parent = transform;
        }

        prefab.transform.parent = transform;


	}
}
