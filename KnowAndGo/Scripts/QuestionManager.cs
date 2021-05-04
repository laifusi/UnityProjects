using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] LevelOfDifficulty difficultyLevel;
    [SerializeField] TextAsset themesFile;

    List<int> possibleIds = new List<int>();
    string[] possibleThemes = new string[3];
    string chosenTheme;

    public static event Action<string> OnTheme1Created;
    public static event Action<string> OnTheme2Created;
    public static event Action<string> OnTheme3Created;
    public static event Action<string> OnQuestionCreated;
    public static event Action<string,bool> OnAnswer1Created;
    public static event Action<string,bool> OnAnswer2Created;
    public static event Action<string,bool> OnAnswer3Created;

    enum LevelOfDifficulty
    {
        Easy,
        Normal,
        Hard
    }
    
    void Start()
    {
        string[] themes = themesFile.text.Split(";"[0]);
        for(int i = 0; i < themes.Length - 1; i++)
        {
            possibleIds.Add(i);
        }

        int randomIdTheme1 = possibleIds[UnityEngine.Random.Range(0, possibleIds.Count)];
        possibleThemes[0] = themes[randomIdTheme1];
        OnTheme1Created?.Invoke(themes[randomIdTheme1]);
        possibleIds.Remove(randomIdTheme1);

        int randomIdTheme2 = possibleIds[UnityEngine.Random.Range(0, possibleIds.Count)];
        possibleThemes[1] = themes[randomIdTheme2];
        OnTheme2Created?.Invoke(themes[randomIdTheme2]);
        possibleIds.Remove(randomIdTheme2);

        int randomIdTheme3 = possibleIds[UnityEngine.Random.Range(0, possibleIds.Count)];
        possibleThemes[2] = themes[randomIdTheme3];
        OnTheme3Created?.Invoke(themes[randomIdTheme3]);
    }

    public void ChooseTheme(int themeNumber)
    {
        chosenTheme = possibleThemes[themeNumber - 1];
        GetQuestion();
    }

    void GetQuestion()
    {
        string[] allQuestions = Resources.Load<TextAsset>($"{chosenTheme}Questions").text.Split("\n"[0]);
        List<string> possibleQuestions = new List<string>();
        for (int i = 0; i < allQuestions.Length; i++)
        {
            string[] questionSections = allQuestions[i].Split(";"[0]);
            string difficulty = questionSections[0];
            if (difficulty == difficultyLevel.GetHashCode().ToString())
            {
                possibleQuestions.Add(questionSections[1]);
            }
        }

        int questionID = UnityEngine.Random.Range(0, possibleQuestions.Count);
        string[] selectedQuestionSections = possibleQuestions[questionID].Split("+"[0]);
        string question = selectedQuestionSections[0];
        OnQuestionCreated?.Invoke(question);

        string[] option1Sections = selectedQuestionSections[1].Split("-"[0]);
        string option1 = option1Sections[0];
        bool is1Right = option1Sections[1] == "correct" ? true : false;
        OnAnswer1Created?.Invoke(option1,is1Right);

        string[] option2Sections = selectedQuestionSections[2].Split("-"[0]);
        string option2 = option2Sections[0];
        bool is2Right = option2Sections[1] == "correct" ? true : false;
        OnAnswer2Created?.Invoke(option2, is2Right);

        string[] option3Sections = selectedQuestionSections[3].Split("-"[0]);
        string option3 = option3Sections[0];
        bool is3Right = option3Sections[1] == "correct" ? true : false;
        OnAnswer3Created?.Invoke(option3,is3Right);
    }
}
