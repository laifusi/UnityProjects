using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float totalTime = 120;
    [SerializeField] GameObject loseAnimation;
    [SerializeField] Player player;

    public static event Action<int> OnTimeChange;
    public static bool stop;

    float timePassed;

    void Start()
    {
        stop = false;
    }

    void Update()
    {
        if(!stop)
        {
            timePassed += Time.deltaTime;
            OnTimeChange?.Invoke((int)(totalTime - timePassed));

            if (timePassed >= totalTime)
            {
                player.LevelEnded();
                loseAnimation.SetActive(true);
            }
        }
    }
}
