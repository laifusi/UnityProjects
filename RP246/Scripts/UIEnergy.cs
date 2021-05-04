using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : MonoBehaviour
{
    Slider slider;
    ColorBlock ourColors;

    void Start()
    {
        slider = GetComponent<Slider>();
        ourColors = slider.colors;

        Robot.OnEnergyChange += ChangeColor;
    }

    void ChangeColor(Color color, float battery)
    {
        slider.value = battery / 100;

        ourColors.disabledColor = color;
        slider.colors = ourColors;
    }

    void OnDisable()
    {
        Robot.OnEnergyChange -= ChangeColor;
    }
}
