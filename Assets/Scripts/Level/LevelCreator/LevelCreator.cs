using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using Tzaik.General;

namespace Tzaik.Level
{
    public partial class LevelCreator : MonoBehaviour
    {
        #region Fields
        [Header("List of Pieces")]
        [SerializeField] ListOfPiecesForArea listTemple;
        [SerializeField] ListOfPiecesForArea listCave;
        [SerializeField] ListOfPiecesForArea listJungle;
        [Header("Pieces")]
        [SerializeField] LevelPiece startPiece;
        [SerializeField] LevelPiece endPiece;
         
        [Header("Settings for the room")]
        [SerializeField] int maxPiecesPerPath;
        [SerializeField] int maxPathNumber;
        [SerializeField] int maxItemsPerRoom;
        [SerializeField] int maxEnemiesPerRoom;
        [SerializeField] int maxCoinsPerRoom;
        [SerializeField] int maxDecorationsPerRoom;
        [SerializeField] int maxTrapsPerRoom;
        [SerializeField] PieceFactory factory;

        [SerializeField] List<LevelPiece> totalPieces;
        [SerializeField] int totalPiecesInCurrentPath;
        List<Transform> currentRemainingConnectors;
        public List<Transform> TotalRemainingConnectors;
        public List<GameObject> piecesToDestroy;
        public List<GameObject> finalPathPieces;
        [SerializeField] int guid;

        int currentPath = 0;
        LevelPiece currentPiece;
        Transform currentConnector; 
        LevelPieceType currentType;
        public Area currentArea;
        ListOfPiecesForArea currentList;
        #endregion

        #region Properties
        public GameObject StartPiece => startPiece.gameObject;
        public bool FinishedCreatingLevel { get; private set; }
        #endregion 
    } 
}
