using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D selectableCursor;

    public static event Action<Color> OnRobotSelected;

    private ISelectableRobot selectedRobot;

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var newSelectedRobot = hit.collider.GetComponent<ISelectableRobot>();
            var selectedTask = hit.collider.GetComponent<TaskObject>();
            var selectedBase = hit.collider.GetComponent<MainControl>();
            if (newSelectedRobot != null || (selectedRobot != null && (selectedTask != null || selectedBase != null)))
                Cursor.SetCursor(selectableCursor, Vector2.zero, CursorMode.Auto);
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {

                //We check for robot
                var newSelectedRobot = hit.collider.GetComponent<ISelectableRobot>();

                if (selectedRobot != newSelectedRobot && newSelectedRobot != null)
                {
                    selectedRobot = newSelectedRobot;
                    OnRobotSelected?.Invoke(hit.collider.GetComponent<Renderer>().material.color);
                }

                //if we have a robot selected, we check for object
                if(selectedRobot != null)
                {
                    var selectedTask = hit.collider.GetComponent<TaskObject>();

                    if (selectedTask != null)
                    {
                        selectedRobot.GoTo(selectedTask);
                        return;
                    }

                    var selectedBase = hit.collider.GetComponent<MainControl>();

                    if(selectedBase != null)
                    {
                        selectedRobot.GoToBase(selectedBase);
                    }
                }
            }
        }
    }
}
