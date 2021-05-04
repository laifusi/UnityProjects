using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, ITakeDamage
{
    [SerializeField] Transform leftSensor;
    [SerializeField] Transform rightSensor;
    [SerializeField] Sprite deadSprite;
    [SerializeField] AudioClip deadSound;

    Rigidbody2D rigidbody2d;
    float direction = -1;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        rigidbody2d.velocity = new Vector2(direction, rigidbody2d.velocity.y);

        if(direction < 0)
        {
            ScanSensor(leftSensor);
        }
        else
        {
            ScanSensor(rightSensor);
        }
    }

    void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);

        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
            TurnAround();

        Debug.DrawRay(sensor.position, new Vector2(direction, 0) * 0.1f, Color.red);

        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(direction, 0), 0.1f);
        if (sideResult.collider != null)
            TurnAround();
    }

    void TurnAround()
    {
        direction *= -1;
        spriteRenderer.flipX = direction > 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        var contact = collision.contacts[0];
        Vector2 normal = contact.normal;

        if (normal.y <= -0.5)
        {
            TakeDamage();
        }
        else
        {
            if (audioSource != null)
                audioSource.Play();
            player.ResetToStart();
        }
    }

    IEnumerator Die()
    {
        spriteRenderer.sprite = deadSprite;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        enabled = false;

        if (audioSource != null)
            audioSource.PlayOneShot(deadSound);

        float alpha = 1;
        while(alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }

    public void TakeDamage()
    {
        StartCoroutine(Die());
    }
}
