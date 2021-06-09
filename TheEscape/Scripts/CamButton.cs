using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamButton : MonoBehaviour
{
    [SerializeField] GameObject[] cameraVisions;
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

        CharacterMovement.OnDied += ResetCameras;
        EscapeSensor.OnWin += TurnCameraOff;
    }

    private void TurnCameraOff()
    {
        audioSource.Stop();
        foreach (GameObject cameraVision in cameraVisions)
            cameraVision.SetActive(false);
    }

    private void ResetCameras()
    {
        foreach (GameObject cameraVision in cameraVisions)
            cameraVision.SetActive(true);
        cameraOff = true;
        cameraTimer = 0;
        audioSource.Stop();
        spriteRenderer.sprite = notPressed;
    }

    void Update()
    {
        if(cameraOff)
        {
            cameraTimer += Time.deltaTime;
            if(cameraTimer >= totalOffTime)
            {
                cameraOff = false;
                foreach(GameObject cameraVision in cameraVisions)
                    cameraVision.SetActive(true);
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
            foreach(GameObject cameraVision in cameraVisions)
                cameraVision.SetActive(false);
            audioSource.Stop();
            cameraOff = false;
            audioSource.PlayOneShot(press);
            cameraTimer = 0;
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

    private void OnDestroy()
    {
        CharacterMovement.OnDied -= ResetCameras;
        EscapeSensor.OnWin -= TurnCameraOff;
    }
}
