using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUpFly : PowerUpBase
{
    [Header("Power Up Fly")]
    public float height = 2f;
    public float animationDuration = .1f;
    public Ease ease = Ease.OutBack;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.ChangeHeight(height, duration, animationDuration, ease);
        PlayerController.Instance.SetPowerUpText("Fly");
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.ResetHeight();
        PlayerController.Instance.SetPowerUpText();
    }

}
