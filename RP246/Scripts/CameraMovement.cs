using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] Transform backLimit;
    [SerializeField] Transform frontLimit;

    void Update()
    {
        var v = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if(transform.position.z + v >= backLimit.position.z && transform.position.z + v <= frontLimit.position.z)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + v);
    }
}
