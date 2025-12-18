using System.Collections;
using System.Collections.Generic;
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
        // StartCoroutine(CreateLevelPiecesCoroutine());

        ClearSpawnedPieces();
        for (int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece();
        }

        var end = Instantiate(endPiece, container);
        end.transform.position = _lastPiecePlaced.endPosition.position;
        _spawnedPieces.Add(end);
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

    IEnumerator CreateLevelPiecesCoroutine()
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

    void Awake()
    {
        // SpawnNextLevel();
        // CreateLevel();
    }

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
