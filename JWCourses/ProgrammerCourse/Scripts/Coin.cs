using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coinsCollected;

    [SerializeField] List<AudioClip> audioClips;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        coinsCollected++;
        ScoreSystem.Add(100);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        if(audioClips.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, audioClips.Count);
            AudioClip clip = audioClips[index];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
