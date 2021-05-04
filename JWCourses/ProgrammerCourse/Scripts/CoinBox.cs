using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] int totalCoins = 3;

    int remainingCoins;

    protected override bool canUse => remainingCoins > 0;

    void Start()
    {
        remainingCoins = totalCoins;
    }

    protected override void Use()
    {
        Coin.coinsCollected++;
        remainingCoins--;
    }
}
