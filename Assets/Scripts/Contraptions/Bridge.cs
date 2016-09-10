using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Bridge : Device {
    public int expandedSize = 8;
    public float totalSeconds = 0.4f;

    private SpriteRenderer sprite;
    private bool active;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        active = false;
    }

    public override void Activate(Component caller)
    {
        if (!active)
        {
            active = true;
            StartCoroutine(Expand());
        }
    }

    public override void Deactivate(Component caller)
    {
        if (active)
        {
            active = false;
            if (sources == 0)
                StartCoroutine(Contract());
        }
    }

    private IEnumerator Expand()
    {
        Vector2 size = sprite.bounds.size;
        Vector2 originalSize = new Vector2(size.x / transform.localScale.x, size.y / transform.localScale.y);

        GameObject prefab = new GameObject();
        SpriteRenderer prefabSprite = prefab.AddComponent<SpriteRenderer>();
        BoxCollider2D prefabCollider = prefab.AddComponent<BoxCollider2D>();
        prefab.layer = 8;
        prefab.transform.position = transform.position;
        prefab.transform.parent = transform;
        prefabSprite.sprite = sprite.sprite;
        prefabCollider.size = new Vector2(size.x, size.y);

        sprite.enabled = false;

        for (int i = 1; i <= expandedSize; i++)
        {
            if (active)
            {
                GameObject child = Instantiate(prefab);
                child.transform.position = transform.position + (new Vector3(originalSize.x * i, 0, 0));
                child.transform.parent = transform;
                yield return new WaitForSeconds(0.1f);
            }
            else
                break;
        }         
    }

    private IEnumerator Contract()
    {
        for(int i = transform.childCount-1; i >= 0; i--)
        {
            if (!active)
            {
                GameObject child = transform.GetChild(i).gameObject;
                Destroy(child);
                yield return new WaitForSeconds(totalSeconds / expandedSize);
            }
            else // Another coroutine must've started, just delete everything quickly. No time for fancy animations!
            {
                GameObject child = transform.GetChild(i).gameObject;
                Destroy(child);
            }
        }
    }
}
