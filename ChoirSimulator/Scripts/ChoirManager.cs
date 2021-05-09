using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoirManager : Singleton<ChoirManager>
{
    private int experience;
    private int money;
    private int happiness;
    private float newSingerRate = 6;
    private float newSingerTime = 2;

    public int Experience => experience;
    public int Money => money;

    private List<Singer> singers;
    private List<SongSO> addedSongs;
    private List<SongSO> learntSongs;

    private List<Singer> sopranos;
    private List<Singer> altos;
    private List<Singer> tenors;
    private List<Singer> bass;

    private int rehearsalsCompleted;
    private int concertsCompleted;

    private int rehearsalsLength;
    private int rehearsalsPrice;
    private int rehearsalsFrequency;
    private List<DayOfWeek> rehearsalDays;
    public List<DayOfWeek> RehearsalDays => rehearsalDays;
    public int MaxRehearsalsLength => 180;
    public int MaxRehearsalsCost => 1000000;
    public int MaxRehearsalsFrequency => 7;

    [SerializeField] private GameObject singerPrefab;
    [SerializeField] private GameObject choirPanel;
    [SerializeField] private GameObject singersPanel;
    [SerializeField] private GameObject songsPanel;

    public EventDataUpdate OnExperienceUpdated;
    public EventDataUpdate OnHappinessUpdated;
    public EventDataUpdate OnMoneyUpdated;
    public EventDataUpdate OnSingerAdded;
    public EventDataUpdate OnSongAdded;
    public EventDataUpdate OnSongLearnt;
    public EventDataUpdate OnConcertAttended;
    public EventDataUpdate OnRehearsalsAttended;
    public EventDataUpdate OnRehearsalsLengthUpdated;
    public EventDataUpdate OnRehearsalsFrequencyUpdated;
    public EventDataUpdate OnRehearsalsCostUpdated;
    public EventDataUpdate OnSopranoAdded;
    public EventDataUpdate OnAltoAdded;
    public EventDataUpdate OnTenorAdded;
    public EventDataUpdate OnBassAdded;

    private int songsCount;
    private int lastConcertCount;
    private const int initialMotivation = 50;

    private void Start()
    {
        singers = new List<Singer>();
        sopranos = new List<Singer>();
        altos = new List<Singer>();
        tenors = new List<Singer>();
        bass = new List<Singer>();
        addedSongs = new List<SongSO>();
        learntSongs = new List<SongSO>();
        rehearsalDays = new List<DayOfWeek>();
        newSingerTime += Time.time;

        InvokeRepeating("UpdateHappiness", 1, 1);
    }

    private void Update()
    {
        if(happiness > 0 && Time.time > newSingerTime && rehearsalsFrequency != 0 && rehearsalsLength != 0)
        {
            newSingerTime = Time.time + newSingerRate;
            NewSinger();
        }
    }

    public void NewSinger()
    {
        
        GameObject singerObject = Instantiate(singerPrefab, choirPanel.transform);
        Singer newSinger = new Singer();
        singers.Add(newSinger);

        if (newSinger.Voice == Voice.Soprano)
        {
            sopranos.Add(newSinger);
            OnSopranoAdded.Invoke(sopranos.Count);
        }
        else if (newSinger.Voice == Voice.Alto)
        {
            altos.Add(newSinger);
            OnAltoAdded.Invoke(altos.Count);
        }
        else if(newSinger.Voice == Voice.Tenor)
        {
            tenors.Add(newSinger);
            OnTenorAdded.Invoke(tenors.Count);
        }
        else if (newSinger.Voice == Voice.Bass)
        {
            bass.Add(newSinger);
            OnBassAdded.Invoke(bass.Count);
        }

        foreach (SongSO song in addedSongs)
        {
            newSinger.LearnSong(song);
        }

        AddMotivation(initialMotivation);

        OnSingerAdded.Invoke(singers.Count);

        AdjustmentsManager.Instance.CheckSongButtonInteractivity();
    }

    public void NewSong(SongSO newSong)
    {
        foreach (Singer singer in singers)
        {
            singer.LearnSong(newSong);
        }
        addedSongs.Add(newSong);

        AddMotivation(5);
        UpdateMoney(-newSong.cost);

        OnSongAdded.Invoke(addedSongs.Count);
    }

    public void AddLearntSong(SongSO song)
    {
        if (learntSongs.Contains(song))
            return;

        learntSongs.Add(song);

        OnSongLearnt.Invoke(learntSongs.Count);
    }

    public void AttendRehearsals()
    {
        foreach (Singer singer in singers)
        {
            singer.AttendReheasals();
        }

        rehearsalsCompleted++;

        AddMotivation(5);

        UpdateMoney(rehearsalsPrice * singers.Count);

        OnRehearsalsAttended.Invoke(rehearsalsCompleted);
    }

    public void AttendConcert(Concert concert)
    {
        foreach (Singer singer in singers)
        {
            singer.AttendConcert(concert);
        }

        AddMotivation(10);
        UpdateMoney(concert.benefit);

        concertsCompleted++;

        OnConcertAttended.Invoke(concertsCompleted);
    }

    private void UpdateMoney(int value)
    {
        uint unsignedMoney = (uint)(money + value);
        if (unsignedMoney >= int.MaxValue)
            money = int.MaxValue;
        else
            money += value;
        OnMoneyUpdated.Invoke(money);

        AdjustmentsManager.Instance.CheckSongButtonInteractivity();
        AdjustmentsManager.Instance.CheckConcertButtonInteractivity();
    }

    public void UpdateExperience()
    {
        experience = 0;

        foreach(Singer singer in singers)
        {
            experience += singer.Experience;
        }

        OnExperienceUpdated.Invoke(experience);
    }

    public void UpdateRehearsalLength(int value)
    {
        rehearsalsLength += value;
        if (rehearsalsLength <=  0)
            rehearsalsLength = 0;
        else if (rehearsalsLength >= MaxRehearsalsLength)
            rehearsalsLength = MaxRehearsalsLength;

        OnRehearsalsLengthUpdated.Invoke(rehearsalsLength);
    }
    public void UpdateRehearsalPrice(int value)
    {
        rehearsalsPrice += value;
        if (rehearsalsPrice < 0)
            rehearsalsPrice = 0;
        else if (rehearsalsPrice > MaxRehearsalsCost)
            rehearsalsPrice = MaxRehearsalsCost;

        OnRehearsalsCostUpdated.Invoke(rehearsalsPrice);
    }

    public void UpdateRehearsalsFrequency(int value)
    {
        rehearsalsFrequency += value;
        if (rehearsalsFrequency < 0)
            rehearsalsFrequency = 0;
        else if (rehearsalsFrequency > MaxRehearsalsFrequency)
            rehearsalsFrequency = MaxRehearsalsFrequency;

        UpdateRehearsalsDays();

        OnRehearsalsFrequencyUpdated.Invoke(rehearsalsFrequency);
    }

    private void UpdateRehearsalsDays()
    {
        rehearsalDays.Clear();
        switch (rehearsalsFrequency)
        {
            case 1:
                rehearsalDays.Add(DayOfWeek.Thursday);
                break;
            case 2:
                rehearsalDays.Add(DayOfWeek.Tuesday);
                rehearsalDays.Add(DayOfWeek.Friday);
                break;
            case 3:
                rehearsalDays.Add(DayOfWeek.Monday);
                rehearsalDays.Add(DayOfWeek.Wednesday);
                rehearsalDays.Add(DayOfWeek.Friday);
                break;
            case 4:
                rehearsalDays.Add(DayOfWeek.Monday);
                rehearsalDays.Add(DayOfWeek.Wednesday);
                rehearsalDays.Add(DayOfWeek.Friday);
                rehearsalDays.Add(DayOfWeek.Saturday);
                break;
            case 5:
                rehearsalDays.Add(DayOfWeek.Monday);
                rehearsalDays.Add(DayOfWeek.Tuesday);
                rehearsalDays.Add(DayOfWeek.Wednesday);
                rehearsalDays.Add(DayOfWeek.Friday);
                rehearsalDays.Add(DayOfWeek.Saturday);
                break;
            case 6:
                rehearsalDays.Add(DayOfWeek.Monday);
                rehearsalDays.Add(DayOfWeek.Tuesday);
                rehearsalDays.Add(DayOfWeek.Wednesday);
                rehearsalDays.Add(DayOfWeek.Thursday);
                rehearsalDays.Add(DayOfWeek.Friday);
                rehearsalDays.Add(DayOfWeek.Saturday);
                break;
            case 7:
                rehearsalDays.Add(DayOfWeek.Monday);
                rehearsalDays.Add(DayOfWeek.Tuesday);
                rehearsalDays.Add(DayOfWeek.Wednesday);
                rehearsalDays.Add(DayOfWeek.Thursday);
                rehearsalDays.Add(DayOfWeek.Friday);
                rehearsalDays.Add(DayOfWeek.Saturday);
                rehearsalDays.Add(DayOfWeek.Sunday);
                break;
        }
    }

    private void UpdateHappiness()
    {
        int singersCount = singers.Count;
        if(singersCount == 0)
            return;

        if (rehearsalsLength == 0 || rehearsalsFrequency == 0)
            happiness -= 10 * singersCount;

        if (songsCount == learntSongs.Count)
            happiness -= 1;
        else
            songsCount = addedSongs.Count;

        if (lastConcertCount == concertsCompleted)
            happiness -= 1;
        else
            lastConcertCount = concertsCompleted;
        
        if(rehearsalsLength != 0)
        {
            if (rehearsalsPrice / rehearsalsLength >= 2)
                happiness -= singersCount;
        }

        if (rehearsalsFrequency > 3)
            happiness -= singersCount;

        if (happiness > 0)
            newSingerRate = 1000 / happiness;
        else if(singers.Count != 0)
            GameManager.Instance.Lose();

        if (newSingerRate < 0.5)
            newSingerRate = 0.5f;

        OnHappinessUpdated.Invoke(happiness);
    }

    private void AddMotivation(int value)
    {
        happiness += value;
        OnHappinessUpdated.Invoke(happiness);
    }

    public int GetNumberOfVoices()
    {
        int voices = 0;

        if (sopranos.Count > 0)
            voices++;

        if (altos.Count > 0)
            voices++;

        if (tenors.Count > 0)
            voices++;

        if (bass.Count > 0)
            voices++;

        return voices;
    }
}

[Serializable] public class EventDataUpdate : UnityEvent<int> { }