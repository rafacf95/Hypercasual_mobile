using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using DG.Tweening;
using System.Linq;

public class EnemyAnimatorManager : Singleton<EnemyAnimatorManager>
{
    public List<EnemyBase> enemies;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetween = .1f;
    public Ease ease = Ease.OutBack;

    void Start()
    {
        enemies = new List<EnemyBase>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartAnimation();
        }
    }

    public void RegisterEnemy(EnemyBase e)
    {
        if (!enemies.Contains(e))
        {
            enemies.Add(e);
            e.transform.localScale = Vector3.zero;
        }
    }

    public void ClearList()
    {
        enemies.Clear();
    }

    private void Sort()
    {
        enemies = enemies.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
        ).ToList();
    }

    public void StartAnimation()
    {
        StartCoroutine(ScaleByTime());
    }

    IEnumerator ScaleByTime()
    {
        foreach (var e in enemies)
        {
            e.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return null;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetween);
        }
    }

}
