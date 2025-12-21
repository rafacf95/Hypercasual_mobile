using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOLevelPieceSetup : ScriptableObject
{
    [Header("Pieces")]
    public List<LevelPieceBase> startPieces;
    public List<LevelPieceBase> middlePieces;
    public int startPiecesNumber = 2;
    public int middlePiecesNumber = 5;
    public float timeBetweenPieces = .3f;
    public List<ArtManager.ArtType> artTypes;

}
