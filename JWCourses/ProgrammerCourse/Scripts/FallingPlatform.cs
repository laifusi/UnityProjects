using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool playerInside;

    HashSet<Player> playersInTrigger = new HashSet<Player>();
    Coroutine coroutine;
    Vector3 initialPosition;
    float wiggleTimer;
    bool falling;

    [Tooltip("Reset the wiggle timer when no player is on the platform")]
    [SerializeField] bool resetOnEmpty;
    [SerializeField] float fallSpeed = 9;
    [Range(0.1f, 5)] [SerializeField] float fallAfterSeconds = 3;
    [Range(0.005f,0.1f)] [SerializeField] float shakeX = 0.05f;
    [Range(0.005f,0.1f)] [SerializeField] float shakeY = 0.05f;

    void Start()
    {
        initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        playersInTrigger.Add(player);

        playerInside = true;

        if(playersInTrigger.Count == 1)
            coroutine = StartCoroutine(WiggleAndFall());
    }

    IEnumerator WiggleAndFall()
    {
        yield return new WaitForSeconds(0.25f);

        while(wiggleTimer < fallAfterSeconds)
        {
            float randomX = UnityEngine.Random.Range(-shakeX, shakeX);
            float randomY = UnityEngine.Random.Range(-shakeY, shakeY);
            transform.position = initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay);
            wiggleTimer += randomDelay;
        }

        falling = true;

        foreach(var collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }

        float fallTimer = 0;
        while(fallTimer < 3)
        {
            transform.position += Vector3.down * Time.deltaTime * fallSpeed;
            fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (falling)
            return;

        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        playersInTrigger.Remove(player);

        if(playersInTrigger.Count == 0)
        {
            playerInside = false;
            StopCoroutine(coroutine);

            if (resetOnEmpty)
                wiggleTimer = 0;
        }
    }
}
