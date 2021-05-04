using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float velocity = 5;
    [SerializeField] float minScale = 0.25f;
    [SerializeField] float acceleration = 1;
    [SerializeField] float deceleration = 1;
    [SerializeField] Sprite shieldedSprite;

    public int NumberOfShields { get; private set; }

    float maxXScale;
    float maxYScale;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    Sprite unshieldedSprite;

    void Start()
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        maxXScale = screenToWorld.x * 2;
        maxYScale = screenToWorld.y * 2;

        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        unshieldedSprite = spriteRenderer.sprite;
    }
    
    void Update()
    {
        /*var xScale = Input.GetAxis("Horizontal") * Time.deltaTime * scaleMultiplier;
        if ((xScale > 0 && transform.localScale.x < maxXScale) || (xScale < 0 && transform.localScale.x > minScale))
        {
            transform.localScale += new Vector3(xScale, 0, 0);
        }

        var yScale = Input.GetAxis("Vertical") * Time.deltaTime * scaleMultiplier;
        if ((yScale > 0 && transform.localScale.y < maxYScale) || (yScale < 0 && transform.localScale.y > minScale))
        {
            transform.localScale += new Vector3(0, yScale, 0);
        }*/

        var x = Input.GetAxis("Horizontal");
        var smoothnessMultiplier = x != 0 ? acceleration : deceleration;
        float newX = Mathf.Lerp(rigidbody2d.velocity.x, x * velocity, Time.deltaTime * smoothnessMultiplier);

        var y = Input.GetAxis("Vertical");
        smoothnessMultiplier = y != 0 ? acceleration : deceleration;
        float newY = Mathf.Lerp(rigidbody2d.velocity.y, y * velocity, Time.deltaTime * smoothnessMultiplier);

        rigidbody2d.velocity = new Vector2(newX, newY);
    }

    internal void PutShieldOn()
    {
        NumberOfShields++;
        spriteRenderer.sprite = shieldedSprite;
    }

    internal void TakeShieldOff()
    {
        NumberOfShields--;
        spriteRenderer.sprite = unshieldedSprite;
    }

    internal void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
