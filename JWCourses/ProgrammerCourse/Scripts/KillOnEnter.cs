using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            if (audioSource != null)
                audioSource.Play();

            player.ResetToStart();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            if(audioSource != null)
                audioSource.Play();

            player.ResetToStart();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            if (audioSource != null)
                audioSource.Play();

            player.ResetToStart();
        }
    }
}
