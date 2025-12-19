using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level")]
    public Transform container;
    public List<GameObject> levels;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPieces;
    public LevelPieceBase endPiece;
    public int piecesNumber = 5;
    public float timeBetweenPieces = .3f;
    public List<ArtManager.ArtType> artTypes;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private int _index;
    private GameObject _currentLevel;
    [SerializeField] private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBase _lastPiecePlaced;

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
        // StartCoroutine(CreateLevelPieceCoroutine());

        ClearSpawnedPieces();
        CoinAnimatorManager.Instance.ClearList();
        EnemyAnimatorManager.Instance.ClearList();
        for (int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece();
        }

        var end = Instantiate(endPiece, container);
        end.transform.position = _lastPiecePlaced.endPosition.position;
        _spawnedPieces.Add(end);

        StartCoroutine(ScalePiecesByTime());
    }
    private void CreateLevelPiece()
    {
        var piece = levelPieces[Random.Range(0, levelPieces.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if (_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            spawnedPiece.transform.position = lastPiece.endPosition.position;
            _lastPiecePlaced = spawnedPiece;
        }
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(RandomArtType()).gameObject);
            ColorManager.Instance.ChangeColorByType(RandomArtType());
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void ClearSpawnedPieces()
    {
        for (int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }
        _spawnedPieces.Clear();
    }

    public ArtManager.ArtType RandomArtType()
    {
        return artTypes[Random.Range(0, artTypes.Count)];
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach (var p in _spawnedPieces)
        {
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        for (int i = 0; i < _spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }

        CoinAnimatorManager.Instance.StartAnimation();
        EnemyAnimatorManager.Instance.StartAnimation();
    }

    IEnumerator CreateLevelPieceCoroutine()
    {
        ClearSpawnedPieces();
        _spawnedPieces = new List<LevelPieceBase>();
        for (int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece();
            yield return new WaitForSeconds(timeBetweenPieces);
        }
        
    }

    #endregion

    void Start()
    {
        CreateLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // SpawnNextLevel();
            CreateLevel();
        }


    }
}
