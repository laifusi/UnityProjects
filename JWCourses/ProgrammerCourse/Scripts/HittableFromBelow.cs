using System;
using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite usedBox;

    Animator animator;
    AudioSource audioSource;

    protected virtual bool canUse => true;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canUse)
            return;

        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            PlayAudio();
            PlayAnimation();
            Use();
            if(!canUse)
                GetComponent<SpriteRenderer>().sprite = usedBox;
        }
    }

    void PlayAudio()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    void PlayAnimation()
    {
        if (animator != null)
            animator.SetTrigger("Use");
    }

    protected abstract void Use();
}
