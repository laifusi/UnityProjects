using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] float bounceVelocity = 10;
    [SerializeField] Sprite downSprite;

    private SpriteRenderer spriteRenderer;
    Sprite upSprite;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        upSprite = spriteRenderer.sprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            var rigidbody2D = player.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, bounceVelocity);
                spriteRenderer.sprite = downSprite;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            spriteRenderer.sprite = upSprite;
        }
    }
}
