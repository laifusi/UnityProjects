using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINewHighscore : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        ScoreSystem.OnHighscoreChange += NewHighscore;

        text = GetComponent<TMP_Text>();
        text.enabled = false;
    }

    void NewHighscore()
    {
        text.enabled = true;
        ScoreSystem.OnHighscoreChange -= NewHighscore;
    }

    void OnDestroy()
    {
        ScoreSystem.OnHighscoreChange -= NewHighscore;
    }
}
