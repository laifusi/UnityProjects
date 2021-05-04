using UnityEngine;

internal interface ISelectableRobot
{
    public void GoTo(TaskObject task);

    public void GoToBase(MainControl mainControl);
}