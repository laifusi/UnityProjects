using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static int score;
    static int highscore;

    public static event Action<int> OnScoreChange;
    public static event Action OnHighscoreChange;

    void Start()
    {
        score = 0;
        highscore = PlayerPrefs.GetInt("Highscore");
    }

    public static void SetPoints(int points)
    {
        score += points;
        OnScoreChange?.Invoke(score);

        if(score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
            OnHighscoreChange?.Invoke();
        }
    }
}
