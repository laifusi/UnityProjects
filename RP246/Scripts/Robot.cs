using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour, ISelectableRobot
{
    [SerializeField] MainControl initialControl;
    [SerializeField] Material headMaterial;
    [SerializeField] Robot otherRobot;
    [SerializeField] float battery = 100;
    [SerializeField] float minSeparationDistance = 5;
    [SerializeField] float distanceCheckRate = 1;
    [SerializeField] Gradient gradient;
    [SerializeField] GameObject loseCanvas;

    public static event Action<Color,float> OnEnergyChange;
    public static event Action OnEnergyIncreasing;
    public static event Action<float> OnEnergyDecreasing;

    private NavMeshAgent agent;
    private float nextDistanceCheckTime;
    private TaskObject task;
    private MainControl mainControl;
    private bool reachedDestination = true;
    private bool hasPath;
    private bool reachedBase = true;
    private bool hasBasePath;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GoToBase(initialControl);
    }

    private void Update()
    {
        if(task != null && !hasPath && !agent.hasPath && !reachedDestination)
        {
            reachedDestination = true;
            task.RobotOnLocation();
        }

        if (hasPath && !agent.hasPath)
        {
            hasPath = false;
        }

        if (mainControl != null && !hasBasePath && !agent.hasPath && !reachedBase)
        {
            reachedBase = true;
        }

        if (hasBasePath && !agent.hasPath)
        {
            hasBasePath = false;
        }

        if (Time.time > nextDistanceCheckTime)
        {
            var distance = Vector3.Distance(transform.position, otherRobot.transform.position);
            if (distance > minSeparationDistance)
            {
                if(battery > 0)
                {
                    LowerBattery(distance);
                }
            }
            else
            {
                if(battery < 100)
                {
                    IncrementBattery(distance);
                }
            }
            nextDistanceCheckTime = Time.time + distanceCheckRate;
            
            var color = gradient.Evaluate(1 - battery/100);
            headMaterial.SetColor("_EmissionColor", color);
            OnEnergyChange?.Invoke(color, battery);
        }
    }

    private void IncrementBattery(float distance)
    {
        battery += distance;
        OnEnergyIncreasing?.Invoke();
    }

    private void LowerBattery(float distance)
    {
        battery -= distance / 5;
        OnEnergyDecreasing?.Invoke(battery);

        if(battery <= 0)
        {
            loseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GoTo(TaskObject selectedTask)
    {
        task?.UnassignRobot();
        mainControl?.UnassignRobot();
        agent.SetDestination(selectedTask.GetTaskPosition());
        task = selectedTask;
        reachedDestination = false;
        hasPath = true;
        task.AssignRobot();
    }

    public void GoToBase(MainControl selectedBase)
    {
        mainControl?.UnassignRobot();
        task?.UnassignRobot();
        agent.SetDestination(selectedBase.GetBasePosition());
        mainControl = selectedBase;
        reachedBase= false;
        hasBasePath = true;
        mainControl.AssignRobot();
    }
}
