using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Concert", menuName = "Concert")]
public class Concert : ScriptableObject
{
    public string location;
    public int lengthInMinutes;
    public int benefit;
    public int cost;
    public int revenue;
    public int experienceNeeded;
    public int songsNeeded;
    public int rehearsalsNeeded;
}
