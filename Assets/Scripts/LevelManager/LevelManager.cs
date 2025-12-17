using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPeieces;
    public LevelPieceBase endPiece;
    public int piecesNumber = 5;
    public float timeBetweenPieces = .3f;

    private int _index;
    private GameObject _currentLevel;
    private List<LevelPieceBase> _spawnedPieces;
    private LevelPieceBase _lastPiece;

    #region Level
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
    #endregion

    #region Pieces

    private void CreateLevel()
    {
        // StartCoroutine(CreateLevelPiecesCoroutine());
        _spawnedPieces = new List<LevelPieceBase>();
        for (int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece();
        }
        var end = Instantiate(endPiece, container);
        end.transform.position = _lastPiece.endPosition.position;
    }
    private void CreateLevelPiece()
    {
        var piece = levelPeieces[Random.Range(0, levelPeieces.Count)];
        var spawned = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            _lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            spawned.transform.position = _lastPiece.endPosition.position;
        }

        _spawnedPieces.Add(spawned);
    }

    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();
        for (int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece();
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }

    #endregion

    void Awake()
    {
        // SpawnNextLevel();
        CreateLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnNextLevel();
        }


    }
}
