using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [SerializeField] private int _index;
    private GameObject _currentLevel;

    private void SpawnNextLevel()
    {

        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if (_index >= levels.Count)
            {
                ResetLevelindex();
            }
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelindex()
    {
        _index = 0;
    }
    void Awake()
    {
        SpawnNextLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnNextLevel();
        }
    }
}
