using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRobot : MonoBehaviour
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();

        Mouse.OnRobotSelected += SelectedRobot;
    }
    
    void SelectedRobot(Color color)
    {
        image.color = color;
    }

    void OnDisable()
    {
        Mouse.OnRobotSelected -= SelectedRobot;
    }
}
