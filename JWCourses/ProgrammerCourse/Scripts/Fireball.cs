using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float launchForce = 5;
    [SerializeField] float bounceForce = 5;

    int bouncesRemaining = 3;
    Rigidbody2D rigidbody2d;

    public int Direction { get; set; }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = new Vector2(launchForce * Direction, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeDamage damageable = collision.collider.GetComponent<ITakeDamage>();
        if(damageable != null)
        {
            damageable.TakeDamage();
            Destroy(gameObject);
            return;
        }

        bouncesRemaining--;
        if (bouncesRemaining < 0)
            Destroy(gameObject);
        else
            rigidbody2d.velocity = new Vector2(launchForce * Direction, bounceForce);
    }
}
