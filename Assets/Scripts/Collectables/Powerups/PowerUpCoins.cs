using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCoins : PowerUpBase
{
    [Header("Coin Collector")]
    public float size = 7f;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(size);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(1);
    }

}
