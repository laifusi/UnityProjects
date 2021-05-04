using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float jumpDelay = 0.5f;
    [SerializeField] float downPull = 0.1f;
    [SerializeField] float acceleration = 1;
    [SerializeField] float deceleration = 1;
    [SerializeField] int maxJumps = 2;
    [SerializeField] Transform feetCheck;
    [SerializeField] GameObject actionText;
    [SerializeField] GameObject winAnimation;
    [SerializeField] GameObject loseAnimation;
    [SerializeField] GameObject playerLight;
    [SerializeField] GameObject hostLight;
    [SerializeField] AudioSource music;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    int jumpsRemaining;
    bool isGrounded;
    float fallTime;
    HashSet<Key> collectedKeys = new HashSet<Key>();
    bool facingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        jumpsRemaining = maxJumps;

        actionText?.SetActive(false);
        winAnimation?.SetActive(false);
        loseAnimation?.SetActive(false);
    }
    
    void Update()
    {
        var hit = Physics2D.OverlapCircle(feetCheck.position, 0.1f, LayerMask.GetMask("Floor"));
        isGrounded = hit != null;

        var x = Input.GetAxis("Horizontal");
        var smoothnessMultiplier = x != 0 ? acceleration : deceleration;
        float newX = Mathf.Lerp(rb.velocity.x, x * speed, Time.deltaTime * smoothnessMultiplier);
        rb.velocity = new Vector2(newX, rb.velocity.y);
        animator.SetBool("walking", x != 0);
        if((facingRight && x < 0) || (!facingRight && x > 0))
            Flip();

        if(Input.GetButtonDown("MyJump") && jumpsRemaining > 0 && (isGrounded || fallTime < jumpDelay))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsRemaining--;
            fallTime = 0;
            audioSource.Play();
        }

        animator.SetBool("jumping", !isGrounded);

        if (isGrounded && fallTime > 0.1f)
        {
            jumpsRemaining = maxJumps;
            fallTime = 0;
        }
        else
        {
            fallTime += Time.deltaTime;
            var downForce = downPull * fallTime * fallTime;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - downForce);
        }
    }

    void Flip()
    {
        transform.Rotate(Vector3.up * 180);
        facingRight = !facingRight;
        actionText.transform.Rotate(Vector3.up * 180);
    }

    public void PickUpKey(Key key)
    {
        collectedKeys.Add(key);
    }

    public bool HasKey(Key key)
    {
        return collectedKeys.Contains(key);
    }

    public void ActivateActionText(bool active)
    {
        actionText?.SetActive(active);
    }

    public void EndAnimation(bool correctAnswer)
    {
        if (correctAnswer)
            winAnimation?.SetActive(true);
        else
            loseAnimation?.SetActive(true);

        LevelEnded();
    }

    public void LevelEnded()
    {
        playerLight.SetActive(false);
        hostLight.SetActive(false);
        music.enabled = false;
        enabled = false;
    }
}
