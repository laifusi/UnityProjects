using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] TaskObject controlPanel;

    Image image;

    void Start()
    {
        image = GetComponent<Image>();

        controlPanel.OnTaskActivated += TaskActivated;
        controlPanel.OnTaskFixed += TaskFixed;
    }

    void TaskActivated()
    {
        image.color = Color.red;
    }

    void TaskFixed(TaskObject task)
    {
        image.color = Color.green;
    }

    void OnDisable()
    {
        controlPanel.OnTaskActivated -= TaskActivated;
        controlPanel.OnTaskFixed -= TaskFixed;
    }
}
