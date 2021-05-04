using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEndCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text highscoreText;

    Canvas canvas;

    void Start()
    {
        Enemy.OnGameLost += ActivateCanvas;

        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    void ActivateCanvas()
    {
        canvas.enabled = true;
        highscoreText.SetText(PlayerPrefs.GetInt("Highscore").ToString());
        GetComponent<AudioSource>().Play();
    }

    void OnDestroy()
    {
        Enemy.OnGameLost -= ActivateCanvas;
    }
}
