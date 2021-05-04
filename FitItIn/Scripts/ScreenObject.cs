using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenObject : MonoBehaviour
{
    [SerializeField] GameObject wallPrefab;

    void Start()
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var xScale = screenToWorld.x * 2;
        var yScale = screenToWorld.y * 2;
        transform.localScale = new Vector3(xScale, yScale, 1);
        var width = screenToWorld.x;
        var height = screenToWorld.y;

        var rightWall = Instantiate(wallPrefab, new Vector2(width, 0), Quaternion.identity);
        var leftWall = Instantiate(wallPrefab, new Vector2(-width, 0), Quaternion.identity);
        var topWall = Instantiate(wallPrefab, new Vector2(0, height), Quaternion.identity);
        var bottomWall = Instantiate(wallPrefab, new Vector2(0, -height), Quaternion.identity);
        rightWall.transform.localScale = new Vector2(0.1f, yScale);
        leftWall.transform.localScale = new Vector2(0.1f, yScale);
        topWall.transform.localScale = new Vector2(xScale, 0.1f);
        bottomWall.transform.localScale = new Vector2(xScale, 0.1f);
    }
}
