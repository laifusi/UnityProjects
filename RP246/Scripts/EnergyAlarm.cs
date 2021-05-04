using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyAlarm : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Robot.OnEnergyIncreasing += StopAlarm;
        Robot.OnEnergyDecreasing += ChangeVolume;
    }

    void StopAlarm()
    {
        audioSource.volume = 0;
    }

    void ChangeVolume(float battery)
    {
        audioSource.volume = 1 - battery / 100;
        if(battery <= 0)
        {
            audioSource.Stop();
        }
    }

    void OnDisable()
    {
        Robot.OnEnergyIncreasing -= StopAlarm;
        Robot.OnEnergyDecreasing -= ChangeVolume;
    }
}
