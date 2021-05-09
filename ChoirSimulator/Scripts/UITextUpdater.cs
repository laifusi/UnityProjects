using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextUpdater : MonoBehaviour
{
    [SerializeField] private EventType eventType;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        switch(eventType)
        {
            case EventType.NumberOfSingers:
                ChoirManager.Instance.OnSingerAdded.AddListener(UpdateText);
                break;
            case EventType.NumberOfSongsAdded:
                ChoirManager.Instance.OnSongAdded.AddListener(UpdateText);
                break;
            case EventType.NumberOfSongsLearnt:
                ChoirManager.Instance.OnSongLearnt.AddListener(UpdateText);
                break;
            case EventType.NumberOfConcerts:
                ChoirManager.Instance.OnConcertAttended.AddListener(UpdateText);
                break;
            case EventType.OverallExperience:
                ChoirManager.Instance.OnExperienceUpdated.AddListener(UpdateText);
                break;
            case EventType.OverallHappiness:
                ChoirManager.Instance.OnHappinessUpdated.AddListener(UpdateText);
                break;
            case EventType.Money:
                ChoirManager.Instance.OnMoneyUpdated.AddListener(UpdateText);
                break;
            case EventType.RehearsalsCost:
                ChoirManager.Instance.OnRehearsalsCostUpdated.AddListener(UpdateText);
                break;
            case EventType.RehearsalsLength:
                ChoirManager.Instance.OnRehearsalsLengthUpdated.AddListener(UpdateText);
                break;
            case EventType.RehearsalsFrequency:
                ChoirManager.Instance.OnRehearsalsFrequencyUpdated.AddListener(UpdateText);
                break;
            case EventType.NumberOfRehearsals:
                ChoirManager.Instance.OnRehearsalsAttended.AddListener(UpdateText);
                break;
            case EventType.NumberOfSopranos:
                ChoirManager.Instance.OnSopranoAdded.AddListener(UpdateText);
                break;
            case EventType.NumberOfAltos:
                ChoirManager.Instance.OnAltoAdded.AddListener(UpdateText);
                break;
            case EventType.NumberOfTenors:
                ChoirManager.Instance.OnTenorAdded.AddListener(UpdateText);
                break;
            case EventType.NumberOfBass:
                ChoirManager.Instance.OnBassAdded.AddListener(UpdateText);
                break;
        }
    }

    private void UpdateText(int newValue)
    {
        text.SetText(newValue.ToString());
    }
}

public enum EventType
{
    NumberOfSingers,
    NumberOfSongsAdded,
    NumberOfSongsLearnt,
    NumberOfConcerts,
    OverallExperience,
    OverallHappiness,
    Money,
    RehearsalsCost,
    RehearsalsLength,
    RehearsalsFrequency,
    NumberOfRehearsals,
    NumberOfSopranos,
    NumberOfAltos,
    NumberOfTenors,
    NumberOfBass
}
