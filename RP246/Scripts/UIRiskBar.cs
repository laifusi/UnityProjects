using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRiskBar : MonoBehaviour
{
    [SerializeField] TaskObject controlPanel;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        controlPanel.OnRiskIncrease += RiskIncrease;
    }

    void RiskIncrease(float danger)
    {
        slider.value = danger/100;
    }

    void OnDisable()
    {
        controlPanel.OnRiskIncrease -= RiskIncrease;
    }
}
