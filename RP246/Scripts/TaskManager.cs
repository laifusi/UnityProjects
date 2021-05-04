using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] TaskObject[] allControlPanels;
    [SerializeField] int difficultyLevel;
    [SerializeField] float taskActivationRate = 2;
    [SerializeField] GameObject winCanvas;
    [SerializeField] int lastLevel = 15;
    [SerializeField] float minTaskActivationRate = 4;

    private List<TaskObject> activePanels = new List<TaskObject>();
    private List<TaskObject> nonactivePanels = new List<TaskObject>();
    private float nextTaskActivationTime;

    private void Start()
    {
        Time.timeScale = 1;

        nextTaskActivationTime = Random.Range(3, 5) + Time.time;

        foreach(TaskObject controlPanel in allControlPanels)
        {
            nonactivePanels.Add(controlPanel);
            controlPanel.OnTaskFixed += TaskFixed;
        }
    }

    void Update()
    {
        if(Time.time >= nextTaskActivationTime && nonactivePanels.Count != 0)
        {
            nextTaskActivationTime = Time.time + taskActivationRate;
            int randomIndex = Random.Range(0, nonactivePanels.Count);
            TaskObject selectedPanel = nonactivePanels[randomIndex];
            selectedPanel.Activate();
            nonactivePanels.Remove(selectedPanel);
            activePanels.Add(selectedPanel);
        }
    }

    private void IncreaseDifficulty()
    {
        difficultyLevel++;

        if(taskActivationRate > minTaskActivationRate)
            taskActivationRate--;

        if(difficultyLevel + activePanels.Count >= lastLevel)
        {
            taskActivationRate = 100;
        }

        if(difficultyLevel >= lastLevel)
        {
            winCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void TaskFixed(TaskObject task)
    {
        nonactivePanels.Add(task);
        activePanels.Remove(task);
        IncreaseDifficulty();
    }

    private void OnDisable()
    {
        foreach(TaskObject controlPanel in allControlPanels)
        {
            controlPanel.OnTaskFixed -= TaskFixed;
        }
    }
}
