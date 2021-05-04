using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action OnGameLost;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (player.NumberOfShields > 0)
            {
                player.TakeShieldOff();
            }
            else
            {
                player.PlaySound();
                OnGameLost?.Invoke();
                Time.timeScale = 0;
                player.enabled = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var screen = collision.GetComponent<ScreenObject>();
        if (screen != null)
        {
            ScoreSystem.SetPoints(5);
            Destroy(gameObject);
        }
    }
}
