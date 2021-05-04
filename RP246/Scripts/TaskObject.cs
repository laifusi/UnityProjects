using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObject : MonoBehaviour
{
    [SerializeField] Transform taskTransform;
    [SerializeField] float taskFixingTime = 10;
    [SerializeField] float taskDangerMultiplier = 2;
    [SerializeField] float taskRate = 1;
    [SerializeField] Light sideLight;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip alarmSound;
    [SerializeField] AudioClip fixingSound;
    [SerializeField] GameObject loseCanvas;

    public event Action<TaskObject> OnTaskFixed;
    public event Action OnTaskActivated;
    public event Action<float> OnRiskIncrease;
    public event Action<float> OnTaskBeingFixed;

    private int robotsFixing;
    private float nextTaskTime;
    private float taskDanger;
    private Collider coll;
    private bool active;
    private Color originalLightColor;

    private void Start()
    {
        coll = GetComponent<Collider>();
        originalLightColor = sideLight.color;
    }

    private void Update()
    {
        if(active && Time.time >= nextTaskTime)
        {
            nextTaskTime = Time.time + taskRate;

            if (robotsFixing > 0)
            {
                FixTask();
            }
            else
            {
                IncrementTaskDanger();
            }
        }
    }

    private void IncrementTaskDanger()
    {
        taskDanger += taskDangerMultiplier;
        audioSource.volume = taskDanger/100;
        OnRiskIncrease?.Invoke(taskDanger);
        if(taskDanger >= 100)
        {
            loseCanvas.SetActive(true);
            audioSource.Stop();
            Time.timeScale = 0;
        }
    }

    private void FixTask()
    {
        taskFixingTime -= robotsFixing;
        OnTaskBeingFixed?.Invoke(taskFixingTime);

        if(taskFixingTime <= 0)
        {
            OnTaskFixed?.Invoke(this);
            sideLight.color = originalLightColor;
            active = false;
            taskFixingTime = 10;
            taskDanger = 0;
            OnTaskBeingFixed?.Invoke(taskFixingTime);
            OnRiskIncrease?.Invoke(taskDanger);
            audioSource.Stop();
        }
    }

    public Vector3 GetTaskPosition()
    {
        return taskTransform.position;
    }

    public void AssignRobot()
    {
        coll.enabled = false;
    }
    public void RobotOnLocation()
    {
        robotsFixing = 1;
        audioSource.clip = fixingSound;
        audioSource.volume = 0.5f;
        if (active)
        {
            audioSource.Play();
        }
    }

    public void UnassignRobot()
    {
        robotsFixing = 0;
        coll.enabled = true;
        if (active)
        {
            audioSource.clip = alarmSound;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void Activate()
    {
        active = true;
        sideLight.color = Color.red;
        OnTaskActivated?.Invoke();
        if(robotsFixing == 0)
        {
            audioSource.clip = alarmSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = fixingSound;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }
    }
}
