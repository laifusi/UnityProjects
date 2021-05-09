using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdjustmentsUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] adjustmentPanels;
    [SerializeField] private Transform adjustmentTabs;
    [SerializeField] private Transform adjustmentTabsUpPosition;
    [SerializeField] private Transform adjustmentTabsDownPosition;
    [SerializeField] private GameObject closeButton;

    private Image previousButtonPressed;
    private Color initialColor;

    public void ToggleEverythingOff()
    {
        for(int i = 0; i < adjustmentPanels.Length; i++)
        {
            adjustmentPanels[i].SetActive(false);
        }
        adjustmentTabs.position = adjustmentTabsDownPosition.position;
        closeButton.SetActive(false);

        if(previousButtonPressed != null)
        {
            previousButtonPressed.color = initialColor;
            previousButtonPressed = null;
        }
    }

    public void ToggleOneOn(GameObject panel)
    {
        ToggleEverythingOff();

        previousButtonPressed = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        initialColor = previousButtonPressed.color;
        previousButtonPressed.color = Color.black;

        adjustmentTabs.position = adjustmentTabsUpPosition.position;
        panel.SetActive(true);
        closeButton.SetActive(true);
    }
}