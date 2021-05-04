using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerNumber = 1;
    [Header("Movement")]
    [SerializeField] float speed = 1;
    [SerializeField] float slipFactor = 1;
    [SerializeField] float acceleration = 1;
    [SerializeField] float deceleration = 1;
    [SerializeField] float airAcceleration = 1;
    [SerializeField] float airDeceleration = 1;
    [Header("Jump")]
    [SerializeField] float jumpVelocity = 10;
    [SerializeField] int maxJumps = 2;
    [SerializeField] Transform feet;
    [SerializeField] float downPull = 0.1f;
    [SerializeField] float maxJumpDuration = 0.1f;
    [Header("Slide")]
    [SerializeField] Transform leftSensor;
    [SerializeField] Transform rightSensor;
    [SerializeField] float wallSlideSpeed = 1;

    Vector3 startPosition;
    int jumpsRemaining;
    float fallTimer;
    float jumpTimer;
    Rigidbody2D rigidbody2d;
    Animator animator;
    SpriteRenderer spriteRenderer;
    float horizontal;
    bool isGrounded;
    bool slipperyGround;
    string jumpButton;
    string horizontalAxis;
    int layerMask;
    AudioSource audioSource;

    public int PlayerNumber => playerNumber;

    void Start()
    {
        startPosition = transform.position;
        jumpsRemaining = maxJumps;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        jumpButton = $"P{playerNumber}Jump";
        horizontalAxis = $"P{playerNumber}Horizontal";
        layerMask = LayerMask.GetMask("Default");
    }

    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();

        if (slipperyGround)
            SlipHorizontal();
        else
            MoveHorizontal();

        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldSlide())
        {
            if (ShouldStartJump())
                WallJump();
            else
                Slide();
            return;
        }

        if (ShouldStartJump())
            Jump();
        else if (ShouldContinueJump())
            ContinueJump();

        jumpTimer += Time.deltaTime;

        if (isGrounded && fallTimer > 0)
        {
            fallTimer = 0;
            jumpsRemaining = maxJumps;
        }
        else
        {
            fallTimer += Time.deltaTime;
            var downForce = downPull * fallTimer * fallTimer;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, rigidbody2d.velocity.y - downForce);
        }
    }

    void WallJump()
    {
        rigidbody2d.velocity = new Vector2(-horizontal * jumpVelocity, jumpVelocity * 1.5f);
    }

    void Slide()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -wallSlideSpeed);
    }

    bool ShouldSlide()
    {
        if (isGrounded)
            return false;

        if (rigidbody2d.velocity.y > 0)
            return false;

        if(horizontal < 0)
        {
            var hit = Physics2D.OverlapCircle(leftSensor.position, 0.1f);
            if(hit != null && hit.CompareTag("Wall"))
                return true;
        }
        else if (horizontal > 0)
        {
            var hit = Physics2D.OverlapCircle(rightSensor.position, 0.1f);
            if (hit != null && hit.CompareTag("Wall"))
                return true;
        }

        return false;
    }

    void ContinueJump()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpVelocity);
        fallTimer = 0;
    }

    bool ShouldContinueJump()
    {
        return Input.GetButton(jumpButton) && jumpTimer <= maxJumpDuration;
    }

    void Jump()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpVelocity);
        jumpsRemaining--;
        fallTimer = 0;
        jumpTimer = 0;
        if(audioSource != null)
            audioSource.Play();
    }

    bool ShouldStartJump()
    {
        return Input.GetButtonDown(jumpButton) && jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
        float smoothnessMultiplier = horizontal == 0 ? deceleration : acceleration;

        if(!isGrounded)
            smoothnessMultiplier = horizontal == 0 ? airDeceleration : airAcceleration;

        float newHorizontal = Mathf.Lerp(rigidbody2d.velocity.x, horizontal * speed, Time.deltaTime * smoothnessMultiplier);
        rigidbody2d.velocity = new Vector2(newHorizontal, rigidbody2d.velocity.y);
    }

    void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(horizontal, rigidbody2d.velocity.y);
        var smoothedVelocity = Vector2.Lerp(
            rigidbody2d.velocity,
            desiredVelocity,
            Time.deltaTime / slipFactor);
        rigidbody2d.velocity = smoothedVelocity;
    }

    void ReadHorizontalInput()
    {
        horizontal = Input.GetAxis(horizontalAxis) * speed;
    }

    void UpdateSpriteDirection()
    {
        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0;
        }
    }

    void UpdateAnimator()
    {
        bool walking = horizontal != 0;
        animator.SetBool("walking", walking);
        animator.SetBool("jump", ShouldContinueJump());
        animator.SetBool("sliding", ShouldSlide());
    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, layerMask);
        isGrounded = hit != null;

        slipperyGround = hit?.CompareTag("Slippery") ?? false;
    }

    internal void ResetToStart()
    {
        transform.position = startPosition;
    }

    internal void TeleportTo(Vector3 position)
    {
        rigidbody2d.position = position;
        rigidbody2d.velocity = Vector2.zero;
    }
}
