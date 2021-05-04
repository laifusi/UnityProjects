using System;
using System.Collections;
using UnityEngine;

public class FadingCloud : HittableFromBelow
{
    [SerializeField] float resetTime = 5;

    SpriteRenderer spriteRenderer;
    Collider2D collider2d;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
    }

    protected override void Use()
    {
        spriteRenderer.enabled = false;
        collider2d.enabled = false;

        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(resetTime);
        spriteRenderer.enabled = true;
        collider2d.enabled = true;
    }
}
