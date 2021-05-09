using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Song", menuName = "Song")]
public class SongSO : ScriptableObject
{
    public string title;

    public int numberOfVoices;
    public float lenghtInMinutes;

    public int rehearsalsNeeded;

    public int cost;

    public int rehearsalsSinceAddition;
}
