using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamButton : MonoBehaviour
{
    [SerializeField] GameObject cameraVision;
    [SerializeField] AudioClip press;
    [SerializeField] float totalOffTime;
    [SerializeField] Sprite notPressed;
    [SerializeField] Sprite pressed;

    bool cameraOff;
    float cameraTimer;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(cameraOff)
        {
            cameraTimer += Time.deltaTime;
            if(cameraTimer >= totalOffTime)
            {
                cameraOff = false;
                cameraVision?.SetActive(true);
                spriteRenderer.sprite = notPressed;
                audioSource.Stop();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMovement>();
        if (character != null)
        {
            spriteRenderer.sprite = pressed;
            cameraVision?.SetActive(false);
            audioSource.Stop();
            cameraOff = false;
            audioSource.PlayOneShot(press);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.GetComponent<CharacterMovement>();
        if(character != null)
        {
            cameraOff = true;
            cameraTimer = 0;
            audioSource.Play();
        }
    }
}
