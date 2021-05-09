using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonInteractivity : MonoBehaviour
{
    [SerializeField] EventType eventType;
    [SerializeField] bool isAdditive;

    Button button;
    int valueToReach;

    private void Start()
    {
        button = GetComponent<Button>();

        switch (eventType)
        {
            case EventType.RehearsalsCost:
                if(isAdditive)
                    valueToReach = ChoirManager.Instance.MaxRehearsalsCost;
                ChoirManager.Instance.OnRehearsalsCostUpdated.AddListener(UpdateButtonInteractivity);
                break;
            case EventType.RehearsalsLength:
                if (isAdditive)
                    valueToReach = ChoirManager.Instance.MaxRehearsalsLength;
                ChoirManager.Instance.OnRehearsalsLengthUpdated.AddListener(UpdateButtonInteractivity);
                break;
            case EventType.RehearsalsFrequency:
                if (isAdditive)
                    valueToReach = ChoirManager.Instance.MaxRehearsalsFrequency;
                ChoirManager.Instance.OnRehearsalsFrequencyUpdated.AddListener(UpdateButtonInteractivity);
                break;
        }
    }

    private void UpdateButtonInteractivity(int value)
    {
        if((valueToReach > 0 && value >= valueToReach) || (valueToReach == 0 && value <= 0))
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
