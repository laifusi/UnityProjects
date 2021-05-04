using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISolvedBar : MonoBehaviour
{
    [SerializeField] TaskObject controlPanel;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        controlPanel.OnTaskBeingFixed += TaskBeingFixed;
    }

    void TaskBeingFixed(float fixingTimeLeft)
    {
        slider.value = 1 - fixingTimeLeft / 10;
    }

    void OnDisable()
    {
        controlPanel.OnTaskBeingFixed -= TaskBeingFixed;
    }
}
