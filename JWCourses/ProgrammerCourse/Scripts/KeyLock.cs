using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] UnityEvent OnUnlocked;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Unlock()
    {
        if (audioSource != null)
            audioSource.Play();

        OnUnlocked.Invoke();
    }
}
