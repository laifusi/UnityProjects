using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singer
{
    private Voice voice;

    private int experience;
    private int songsLearnt;
    private int rehearsalsAttended;
    private int concertsAttended;
    private float motivation;

    private List<SongSO> songs;
    private List<SongSO> songsToLearn;

    public Voice Voice => voice;


    private const int numberOfVoices = 4;

    public int Experience => experience;

    public Singer()
    {
        int randomVoice = Random.Range(0, numberOfVoices);
        switch(randomVoice)
        {
            case 0:
                voice = Voice.Soprano;
                break;
            case 1:
                voice = Voice.Alto;
                break;
            case 2:
                voice = Voice.Tenor;
                break;
            case 3:
                voice = Voice.Bass;
                break;
        }

        experience = 0;
        songsLearnt = 0;
        rehearsalsAttended = 0;
        concertsAttended = 0;
        motivation = 100;

        songs = new List<SongSO>();
        songsToLearn = new List<SongSO>();
    }

    public void LearnSong(SongSO newSong)
    {
        if (!EnoughRehearsals(newSong))
        {
            songsToLearn.Add(newSong);
            return;
        }

        if (songsToLearn.Contains(newSong))
            songsToLearn.Remove(newSong);

        songsLearnt++;
        songs.Add(newSong);

        ChoirManager.Instance.AddLearntSong(newSong);

        UpdateExperience();
    }

    private bool EnoughRehearsals(SongSO song)
    {
        return rehearsalsAttended >= song.rehearsalsNeeded && song.rehearsalsSinceAddition >= song.rehearsalsNeeded;
    }

    public void AttendReheasals()
    {
        rehearsalsAttended++;
        foreach (SongSO song in songsToLearn)
        {
            song.rehearsalsSinceAddition++;
            if(EnoughRehearsals(song))
            {
                LearnSong(song);
                break;
            }
        }

        UpdateExperience();
    }

    public void AttendConcert(Concert concert)
    {
        if (experience < concert.experienceNeeded)
            return;

        concertsAttended++;
        UpdateExperience();
    }

    private void UpdateExperience()
    {
        experience = songsLearnt + 2 * concertsAttended;

        ChoirManager.Instance.UpdateExperience();
    }
}

public enum Voice
{
    Soprano,
    Alto,
    Tenor,
    Bass
}
