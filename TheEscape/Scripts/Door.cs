using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] AudioClip lockedSound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var inventory = collision.collider.GetComponent<Inventory>();
        if(inventory != null)
        {
            if (inventory.HasKey())
            {
                //Open the door
                GetComponent<AudioSource>().Play();
                GetComponent<Collider2D>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                enabled = false;
            }
            else
            {
                GetComponent<Animator>()?.SetTrigger("NoKey");
                GetComponent<AudioSource>().PlayOneShot(lockedSound);
            }
        }
    }
}
