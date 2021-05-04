using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] float speed = 2;
    [SerializeField] float maxDistance = 2;

    Vector2 startPosition;
    bool onPatrol;

    void Start()
    {
        onPatrol = true;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(onPatrol)
        {
            transform.Translate(direction.normalized * Time.deltaTime * speed);
            var distance = Vector2.Distance(startPosition, transform.position);
            if (distance >= maxDistance)
            {
                transform.position = startPosition + (direction.normalized * maxDistance);
                direction *= -1;
            }
        }
    }

    public void InmateCaught()
    {
        StartCoroutine(WaitForRestart());
    }

    IEnumerator WaitForRestart()
    {
        onPatrol = false;
        yield return new WaitForSeconds(1.5f);
        onPatrol = true;
    }
}
