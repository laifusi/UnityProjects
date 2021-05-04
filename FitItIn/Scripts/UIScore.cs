using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TMP_Text text;
    private AudioSource audioSource;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        ScoreSystem.OnScoreChange += UpdateScoreText;
        UpdateScoreText(0);
    }

    void UpdateScoreText(int score)
    {
        text.SetText(score.ToString());
        if(score != 0)
            audioSource.Play();
    }

    void OnDestroy()
    {
        ScoreSystem.OnScoreChange -= UpdateScoreText;
    }
}
