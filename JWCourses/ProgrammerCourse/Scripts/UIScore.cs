using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        ScoreSystem.OnScoreChanged += UpdateScoreText;
        UpdateScoreText(ScoreSystem.score);
    }

    void OnDestroy()
    {
        ScoreSystem.OnScoreChanged -= UpdateScoreText;
    }

    void UpdateScoreText(int score)
    {
        text.SetText(score.ToString());
    }
}
