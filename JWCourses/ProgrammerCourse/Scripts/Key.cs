using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyLock unlockableKeyLock;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;

            if (audioSource != null)
                audioSource.Play();
        }

        var keyLock = collision.GetComponent<KeyLock>();
        if(keyLock != null && keyLock == unlockableKeyLock)
        {
            keyLock.Unlock();
            Destroy(gameObject);
        }
    }
}
