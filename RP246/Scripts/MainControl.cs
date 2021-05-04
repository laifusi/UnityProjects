using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    [SerializeField] Transform robotPosition;

    private Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
    }

    public void AssignRobot()
    {
        if(coll != null)
            coll.enabled = false;
    }

    public void UnassignRobot()
    {
        coll.enabled = true;
    }

    public Vector3 GetBasePosition()
    {
        return robotPosition.position;
    }
}
