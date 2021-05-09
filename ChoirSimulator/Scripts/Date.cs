using System;
using UnityEngine;
using TMPro;

public class Date : MonoBehaviour
{
    private TMP_Text dateText;

    private DateTime date;
    private int day;
    private int month;
    private int year;
    private DayOfWeek weekDay;
    [SerializeField] private int dayLength = 1;

    private void Awake()
    {
        dateText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        date = DateTime.Today;
        day = date.Day;
        month = date.Month;
        year = date.Year;
        weekDay = date.DayOfWeek;
        dateText.SetText($"{weekDay} {day} - {month} - {year}");

        InvokeRepeating("UpdateDate", dayLength, dayLength);
    }

    private void UpdateDate()
    {
        date = date.AddDays(1);
        day = date.Day;
        month = date.Month;
        year = date.Year;
        weekDay = date.DayOfWeek;

        dateText.SetText($"{weekDay} {day} - {month} - {year}");

        foreach(DayOfWeek rehearsalDay in ChoirManager.Instance.RehearsalDays)
        {
            if(weekDay == rehearsalDay)
            {
                ChoirManager.Instance.AttendRehearsals();
                break;
            }
        }
    }
}
