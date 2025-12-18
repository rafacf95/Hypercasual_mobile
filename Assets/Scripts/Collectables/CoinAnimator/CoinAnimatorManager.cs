using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using DG.Tweening;

public class CoinAnimatorManager : Singleton<CoinAnimatorManager>
{
    public List<ItemCollectableCoin> coins;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenCoins = .1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        coins = new List<ItemCollectableCoin>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartAnimation();
        }
    }

    public void RegisterCoin(ItemCollectableCoin c)
    {
        if (!coins.Contains(c))
        {
            coins.Add(c);
            c.transform.localScale = Vector3.zero;
        }
    }

    public void RemoveCoin(ItemCollectableCoin c)
    {
        if (coins.Contains(c))
        {
            coins.Remove(c);
        }
    }

    public void ClearList()
    {
        coins.Clear();
    }

    private void Sort()
    {
        coins = coins.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
            ).ToList();
    }

    public void StartAnimation()
    {
        StartCoroutine(ScaleCoinByTime());
    }

    IEnumerator ScaleCoinByTime()
    {
        foreach (var c in coins)
        {
            c.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return null;

        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenCoins);
        }
    }
}
