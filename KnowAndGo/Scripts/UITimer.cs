using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        Timer.OnTimeChange += UpdateTimer;
        text = GetComponent<TMP_Text>();
    }

    void UpdateTimer(int time)
    {
        int m = time / 60;
        int s = time - 60*m;

        if(s < 10)
            text.SetText($"{m}:0{s}");
        else
            text.SetText($"{m}:{s}");
    }

    void OnDestroy()
    {
        Timer.OnTimeChange -= UpdateTimer;
    }
}
