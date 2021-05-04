using TMPro;
using UnityEngine;

public class UIAnswer : MonoBehaviour
{
    [SerializeField] int answerNumber = 1;
    
    void Start()
    {
        switch(answerNumber)
        {
            case 1:
                QuestionManager.OnAnswer1Created += WriteAnswer;
                break;
            case 2:
                QuestionManager.OnAnswer2Created += WriteAnswer;
                break;
            case 3:
                QuestionManager.OnAnswer3Created += WriteAnswer;
                break;
        }
    }
    
    void WriteAnswer(string answer, bool isCorrect)
    {
        GetComponent<TMP_Text>().SetText(answer);
    }

    void OnDestroy()
    {
        switch (answerNumber)
        {
            case 1:
                QuestionManager.OnAnswer1Created -= WriteAnswer;
                break;
            case 2:
                QuestionManager.OnAnswer2Created -= WriteAnswer;
                break;
            case 3:
                QuestionManager.OnAnswer3Created -= WriteAnswer;
                break;
        }
    }
}
