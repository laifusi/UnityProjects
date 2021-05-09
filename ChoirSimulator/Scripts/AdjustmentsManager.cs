using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdjustmentsManager : Singleton<AdjustmentsManager>
{
    [SerializeField] private SongSO songTemplate;
    [SerializeField] private Concert concertTemplate;
    [SerializeField] private string[] songNamePossibilities;
    [SerializeField] private string[] locationPossibilities;
    [SerializeField] private Button[] songButtons;
    [SerializeField] private Button[] concertButtons;

    private bool initialSetup;
    private SongSO[] songs;
    private Concert[] concerts;
    private int maxConcertLength = 11;
    private int maxRevenueMultiplier;
    private int maxCostMultiplier = 11;
    private int maxXPNeeded = 100;
    private int songCostMultiplier = 1;

    private void Start()
    {
        songs = new SongSO[songButtons.Length];
        concerts = new Concert[concertButtons.Length];

        initialSetup = true;

        for(int i = 0; i < songButtons.Length; i++)
        {
            CreateNewSong(i);
        }

        for (int i = 0; i < concertButtons.Length; i++)
        {
            CreateNewConcert(i);
        }

        initialSetup = false;

        CheckSongButtonInteractivity();
        CheckConcertButtonInteractivity();
    }

    public void CreateNewSong(int buttonID)
    {
        if(!initialSetup && !BuySong(buttonID))
        {
            return;
        }

        SongSO song;

        song = Instantiate(songTemplate);
        song.title = songNamePossibilities.Length != 0 ? songNamePossibilities[Random.Range(0, songNamePossibilities.Length)] : "";
        song.numberOfVoices = Random.Range(1,4);
        song.lenghtInMinutes = Random.Range(1,11);
        song.rehearsalsNeeded = (int)((song.numberOfVoices + song.lenghtInMinutes) / 2) * Random.Range(1, songCostMultiplier);
        song.cost = song.rehearsalsNeeded * song.rehearsalsNeeded * 10;

        songs[buttonID] = song;

        TMP_Text[] texts = songButtons[buttonID].GetComponentsInChildren<TMP_Text>();
        texts[0].SetText(song.title);
        texts[1].SetText("Voices: " + song.numberOfVoices.ToString());
        texts[2].SetText("Length: " + song.lenghtInMinutes.ToString());
        texts[3].SetText("Difficulty: " + song.rehearsalsNeeded.ToString());
        texts[4].SetText("Cost: " + song.cost.ToString());

        if (!initialSetup)
        {
            CheckSongButtonInteractivity();
        }
    }

    private bool BuySong(int songID)
    {
        int money = ChoirManager.Instance.Money;
        int cost = songs[songID].cost;

        if (money >= cost)
        {
            ChoirManager.Instance.NewSong(songs[songID]);
            if (songCostMultiplier < 50)
                songCostMultiplier += 1;

            return true;
        }

        return false;
    }

    public void CreateNewConcert(int buttonID)
    {
        if (!initialSetup && !BuyConcert(buttonID))
        {
            return;
        }

        Concert concert;

        concert = Instantiate(concertTemplate);
        concert.location = locationPossibilities.Length != 0 ? locationPossibilities[Random.Range(0, locationPossibilities.Length)] : "";
        concert.lengthInMinutes = Random.Range(10, maxConcertLength);
        concert.cost = concert.lengthInMinutes * Random.Range(10, maxCostMultiplier);
        concert.revenue = Random.Range(0, maxRevenueMultiplier) * concert.lengthInMinutes;
        concert.benefit = concert.revenue - concert.cost;
        concert.experienceNeeded = Random.Range(10, maxXPNeeded);

        concerts[buttonID] = concert;

        TMP_Text[] texts = concertButtons[buttonID].GetComponentsInChildren<TMP_Text>();
        texts[0].SetText("Location: " + concert.location);
        texts[1].SetText("Length: " + concert.lengthInMinutes.ToString());
        texts[2].SetText("Benefit: " + concert.benefit.ToString());
        texts[3].SetText("XP Needed: " + concert.experienceNeeded.ToString());

        if(!initialSetup)
        {
            CheckConcertButtonInteractivity();
        }
    }

    private bool BuyConcert(int concertID)
    {
        int money = ChoirManager.Instance.Money;
        int benefit = concerts[concertID].benefit;
        int experience = ChoirManager.Instance.Experience;
        int experienceNeeded = concerts[concertID].experienceNeeded;

        if ((benefit > 0 || money >= Mathf.Abs(benefit)) && experience > experienceNeeded)
        {
            ChoirManager.Instance.AttendConcert(concerts[concertID]);
            if(maxConcertLength < 120)
                maxConcertLength += 30;

            if(maxRevenueMultiplier < 50)
                maxRevenueMultiplier += 10;

            if(maxCostMultiplier < 50)
                maxCostMultiplier += 10;

            maxXPNeeded += 100;

            return true;
        }

        return false;
    }

    public void CheckSongButtonInteractivity()
    {
        for(int i = 0; i < songs.Length; i++)
        {
            if (EnoughMoney(songs[i].cost) && EnoughVoices(songs[i].numberOfVoices))
            {
                songButtons[i].interactable = true;
            }
            else
            {
                songButtons[i].interactable = false;
            }
        }
    }

    public void CheckConcertButtonInteractivity()
    {
        for (int i = 0; i < concerts.Length; i++)
        {
            if ((concerts[i].benefit >= 0 || EnoughMoney(-concerts[i].benefit)) && EnoughExperience(concerts[i].experienceNeeded))
            {
                concertButtons[i].interactable = true;
            }
            else
            {
                concertButtons[i].interactable = false;
            }
        }
    }

    private bool EnoughMoney(int cost)
    {
        return ChoirManager.Instance.Money >= cost;
    }

    private bool EnoughVoices(int voicesNeeded)
    {
        return ChoirManager.Instance.GetNumberOfVoices() >= voicesNeeded;
    }

    private bool EnoughExperience(int xpNeeded)
    {
        return ChoirManager.Instance.Experience >= xpNeeded;
    }
}
