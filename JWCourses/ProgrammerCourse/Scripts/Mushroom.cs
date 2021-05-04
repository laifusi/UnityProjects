using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float bounceVelocity = 10;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if(player != null)
        {
            var rigidbody2D = player.GetComponent<Rigidbody2D>();
            if(rigidbody2D != null)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, bounceVelocity);

                if (audioSource != null)
                    audioSource.Play();
            }
        }
    }
}
