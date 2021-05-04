using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Vector2 itemLaunchVelocity;

    bool used;

    protected override bool canUse => !used;

    void Start()
    {
        if (item != null)
            item.SetActive(false);
    }

    protected override void Use()
    {
        GameObject item = Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity, transform);

        if (item == null)
            return;

        used = true;
        item.SetActive(true);
        var itemRB = item.GetComponent<Rigidbody2D>();
        if (itemRB != null)
        {
            itemRB.velocity = itemLaunchVelocity;
        }
    }
}
