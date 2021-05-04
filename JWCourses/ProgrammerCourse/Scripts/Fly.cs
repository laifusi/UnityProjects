using UnityEngine;

public class Fly : MonoBehaviour, ITakeDamage
{
    Vector2 startPosition;
    [SerializeField] Vector2 direction = Vector2.up;
    [SerializeField] float maxDistance = 2;
    [SerializeField] float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed);
        var distance = Vector2.Distance(startPosition, transform.position);
        if(distance >= maxDistance)
        {
            transform.position = startPosition + (direction.normalized * maxDistance);
            direction *= -1;
        }
    }

    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }
}