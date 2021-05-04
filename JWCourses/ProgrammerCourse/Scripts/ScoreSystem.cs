using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;

    public static int score { get; private set; }

    void Start()
    {
        score = 0;
    }

    public static void Add(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
        CheckHighScore();
    }

    static void CheckHighScore()
    {
        int highscore = PlayerPrefs.GetInt("Highscore");
        if(score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}