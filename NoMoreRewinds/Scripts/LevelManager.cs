using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int possibleDistractions;
    public float minTimeRange;
    public float maxTimeRange;
    public int n_levelDistractions;

    public GameObject rewindsContainer;
    private Image[] rewinds;
    public Sprite lostRewind;
    private int left_rewinds;

    //public Animator movie;

    public AudioSource rewindSound;

    public GameObject lostGamePanel;
    public GameObject wonGamePanel;

    private int distractions;
    private int activeDistractions = 0;

    public bool levelended = false;

    public Text lostrewinds;
    public Text avoideddistractions;

    private void Start()
    {
        rewinds = rewindsContainer.GetComponentsInChildren<Image>();
        left_rewinds = rewinds.Length;
    }

    private void Update()
    {
        if(distractions >= n_levelDistractions && activeDistractions == 0)
        {
            StartCoroutine(GameWon());
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene("Level" + level);
    }

    public bool AddDistraction()
    {
        bool added = true;
        distractions++;
        if(distractions > n_levelDistractions)
        {
            added = false;
        }
        return added;
    }

    public void SetDistractionActive(int sum)
    {
        activeDistractions += sum; //sum will be 1 or -1
    }

    public int GetDistractionActive()
    {
        return activeDistractions;
    }

    public void Rewind()
    {
        //StartCoroutine(RewindMovie());

        left_rewinds--;
        rewinds[left_rewinds].sprite = lostRewind;

        //rewindSound.Play();

        if(left_rewinds == 0)
        {
            StartCoroutine(GameLost());
        }
    }

    /*IEnumerator RewindMovie()
    {
        float time = movie.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float newTime;
        if(time == 0)
        {
            newTime = 1;
        }
        else if(time == 1)
        {
            newTime = 0;
        }
        else
        {
            newTime = time * 0.8f / 0.2f;
        }
        movie.Play("movie1_reversed",0,newTime);
        yield return new WaitForSeconds(2);
        time = movie.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (time == 0)
        {
            newTime = 1;
        }
        else if (time == 1)
        {
            newTime = 0;
        }
        else
        {
            newTime = time * 0.8f / 0.2f;
        }
        movie.Play("movie1", 0, newTime);
        time = movie.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }*/

    IEnumerator GameWon()
    {
        int lost_rewinds = rewinds.Length - left_rewinds;
        int avoided_distractions = n_levelDistractions - lost_rewinds;

        lostrewinds.text = lost_rewinds.ToString() + "/" + rewinds.Length.ToString();
        avoideddistractions.text = avoided_distractions.ToString() + "/" + n_levelDistractions.ToString();

        levelended = true;
        yield return new WaitForSeconds(2f);
        wonGamePanel.SetActive(true);
    }

    IEnumerator GameLost()
    {
        levelended = true;
        yield return new WaitForSeconds(2f);
        lostGamePanel.SetActive(true);
    }
}