using TMPro;
using UnityEngine;

public class UIQuestion : MonoBehaviour
{
    void Start()
    {
        QuestionManager.OnQuestionCreated += WriteQuestion;
    }

    void WriteQuestion(string question)
    {
        GetComponent<TMP_Text>().SetText(question);
    }

    void OnDestroy()
    {
        QuestionManager.OnQuestionCreated -= WriteQuestion;
    }
}
