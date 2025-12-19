using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BounceHelper : MonoBehaviour
{
    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleBounce = 1.2f;
    public Ease ease = Ease.OutBack;

    private Tween _myTween;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Bounce();
        }
    }

    public void Bounce()
    {
        // if (_myTween == null || !_myTween.IsActive())
        // {
        //     _myTween = transform.DOScale(scaleBounce, scaleDuration).SetEase(ease).SetLoops(2, LoopType.Yoyo);
        // }
        transform.DOScale(scaleBounce, scaleDuration).SetEase(ease).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            transform.localScale = Vector3.one;
        });
    }
}
