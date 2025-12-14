using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    [Header("Coins setup")]
    public SOInt coins;
    public TextMeshProUGUI coinText;

    [Header("Lifes setup")]
    public SOInt lifes;
    public TextMeshProUGUI lifeText;

    private void Reset()
    {
        coins.value = 0;
        lifes.value = 0;
    }

    public void AddCoin(int amount = 1)
    {
        coins.value += amount;
    }

    public void AddLife(int amount = 1)
    {
        lifes.value += amount;
    }
    void Start()
    {
        Reset();
    }
}
