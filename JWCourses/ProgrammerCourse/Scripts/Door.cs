using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite openMid;
    [SerializeField] Sprite openTop;

    [SerializeField] SpriteRenderer rendererMid;
    [SerializeField] SpriteRenderer rendererTop;

    [SerializeField] int requiredCoins = 3;
    [SerializeField] Door exitDoor;

    [SerializeField] Canvas canvas;

    bool open;

    void Update()
    {
        if(!open && Coin.coinsCollected >= requiredCoins)
            Open();
    }

    [ContextMenu("Open Door")]
    public void Open()
    {
        open = true;
        rendererMid.sprite = openMid;
        rendererTop.sprite = openTop;
        if(canvas != null)
            canvas.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!open)
            return;

        var player = collision.GetComponent<Player>();
        if(player != null && exitDoor != null)
        {
            player.TeleportTo(exitDoor.transform.position);
        }
    }
}
