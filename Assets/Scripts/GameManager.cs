using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class GameManager : Singleton<GameManager>
{
    public GameObject startGameUi;
    public GameObject endGameUi;

    void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        startGameUi.SetActive(true);
    }
    public void EndGame()
    {
        endGameUi.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
