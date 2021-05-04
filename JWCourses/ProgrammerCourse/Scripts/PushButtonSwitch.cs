using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] int playerNumber = 1;
    [SerializeField] Sprite pressedSprite;
    [SerializeField] UnityEvent onPressed;
    [SerializeField] UnityEvent onReleased;
    SpriteRenderer spriteRenderer;
    Sprite releasedSprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        releasedSprite = spriteRenderer.sprite;

        BecomeReleased();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null || player.PlayerNumber != playerNumber)
            return;
        BecomePressed();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null || player.PlayerNumber != playerNumber)
            return;

        if(onReleased.GetPersistentEventCount() > 0)
            BecomeReleased();
    }

    void BecomeReleased()
    {
        spriteRenderer.sprite = releasedSprite;
        onReleased?.Invoke();
    }

    void BecomePressed()
    {
        spriteRenderer.sprite = pressedSprite;
        onPressed?.Invoke();
    }
}
