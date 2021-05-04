using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.PickUpKey(this);
            StartCoroutine(Pick());
        }
    }

    IEnumerator Pick()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}