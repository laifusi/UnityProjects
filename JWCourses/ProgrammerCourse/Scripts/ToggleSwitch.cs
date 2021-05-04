using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] ToggleDirection initialDirection = ToggleDirection.Center;

    [SerializeField] UnityEvent _onPushedLeft;
    [SerializeField] UnityEvent _onPushedRight;
    [SerializeField] UnityEvent _onCenter;

    [SerializeField] Sprite _pushedLeft;
    [SerializeField] Sprite _center;
    [SerializeField] Sprite _pushedRight;

    [SerializeField] AudioClip rightSound;
    [SerializeField] AudioClip leftSound;

    SpriteRenderer _spriteRenderer;
    AudioSource audioSource;
    ToggleDirection currentDirection;

    enum ToggleDirection
    {
        Left,
        Center,
        Right
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        SetToggleDirection(initialDirection, true);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        var playerRB = player.GetComponent<Rigidbody2D>();
        if (playerRB == null)
            return;

        bool playerAtRight = transform.position.x < player.transform.position.x;
        bool walkingToRight = playerRB.velocity.x > 0;
        bool walkingToLeft = playerRB.velocity.x < 0;

        if (playerAtRight && walkingToRight)
            SetToggleDirection(ToggleDirection.Right);
        else if(!playerAtRight && walkingToLeft)
            SetToggleDirection(ToggleDirection.Left);
    }

    void SetToggleDirection(ToggleDirection direction, bool force = false)
    {
        if (!force && currentDirection == direction)
            return;

        currentDirection = direction;

        switch(direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = _pushedLeft;
                _onPushedLeft.Invoke();
                if (audioSource != null)
                    audioSource.PlayOneShot(leftSound);
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = _center;
                _onCenter.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = _pushedRight;
                _onPushedRight.Invoke();
                if (audioSource != null)
                    audioSource.PlayOneShot(rightSound);
                break;
        }
    }

    void OnValidate()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        switch (initialDirection)
        {
            case ToggleDirection.Left:
                spriteRenderer.sprite = _pushedLeft;
                break;
            case ToggleDirection.Center:
                spriteRenderer.sprite = _center;
                break;
            case ToggleDirection.Right:
                spriteRenderer.sprite = _pushedRight;
                break;
        }
    }
}
