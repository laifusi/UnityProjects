using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroductionManager : Singleton<IntroductionManager>
{
    [SerializeField] private TMP_Text introText;
    [SerializeField] private IntroStringPosition[] introStringsPositions;

    private int currentString;
    private Transform container;

    private void Start()
    {
        container = introText.transform.parent;
        container.gameObject.SetActive(false);

        Invoke("StartIntro", 1);
    }

    private void StartIntro()
    {
        container.gameObject.SetActive(true);
        FlipTimeScale();
        introText.gameObject.SetActive(true);
        UpdateText();
    }

    public void UpdateText()
    {
        if (currentString >= introStringsPositions.Length)
        {
            FlipTimeScale();
            gameObject.SetActive(false);
            return;
        }

        introText.SetText(introStringsPositions[currentString].introString);
        container.position = introStringsPositions[currentString].introTransform.position;
        currentString++;
    }

    private void FlipTimeScale()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}

[Serializable]
public struct IntroStringPosition
{
    public string introString;
    public Transform introTransform;
}
