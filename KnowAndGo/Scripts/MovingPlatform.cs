using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float xLimit = 0.75f;
    [SerializeField] Vector2 direction = Vector2.up;
    [SerializeField] float maxDistance = 2;
    [SerializeField] float speed = 2;
    [SerializeField] LineRenderer lineRendererPrefab;

    Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
        var size = direction * maxDistance;
        LineRenderer lineRenderer = Instantiate(lineRendererPrefab, transform.position, Quaternion.identity);
        lineRenderer.SetPosition(0, size);
        lineRenderer.SetPosition(1, -size);
    }
    
    void Update()
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed);
        var distance = Vector2.Distance(startPosition, transform.position);
        if (distance >= maxDistance)
        {
            transform.position = startPosition + (direction.normalized * maxDistance);
            direction *= -1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var normal = collision.contacts[0].normal;

        if (collision.collider.GetComponent<Player>() != null && normal.y < 0 && Mathf.Abs(normal.x) < xLimit)
        {
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() != null && collision.transform.parent == transform)
        {
            collision.transform.parent = transform.parent.parent;
        }
    }
}
