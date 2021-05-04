using TMPro;
using UnityEngine;

public class UITheme : MonoBehaviour
{
    [SerializeField] int themeNumber = 1;
    
    void Awake()
    {
        switch(themeNumber)
        {
            case 1:
                QuestionManager.OnTheme1Created += WriteTheme;
                break;
            case 2:
                QuestionManager.OnTheme2Created += WriteTheme;
                break;
            case 3:
                QuestionManager.OnTheme3Created += WriteTheme;
                break;
        }
    }
    
    void WriteTheme(string theme)
    {
        GetComponent<TMP_Text>().SetText(theme);
    }

    void OnDestroy()
    {
        switch (themeNumber)
        {
            case 1:
                QuestionManager.OnTheme1Created -= WriteTheme;
                break;
            case 2:
                QuestionManager.OnTheme2Created -= WriteTheme;
                break;
            case 3:
                QuestionManager.OnTheme3Created -= WriteTheme;
                break;
        }
    }
}
