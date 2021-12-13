using UnityEngine; 
using System.Collections;
using System.Collections.Generic;
using Tzaik.General;
using Tzaik.UI;

namespace Tzaik.Level
{ 
    public partial class LevelCreator
    {
        #region Unity Methods
        public void Initialize()
        {
            FinishedCreatingLevel = false;
            SetCurrentList();
            TotalRemainingConnectors.Clear();
            StartCoroutine(CreateRemainingPaths()); 
        }

        #endregion

        #region Methods 
        IEnumerator CreateRemainingPaths()
        { 
            while (PathsRemaining())
            {
                totalPiecesInCurrentPath = 0;
                if (currentPath == 0)
                {
                    currentType = LevelPieceType.Room;
                    totalPieces.Add(startPiece);
                    CreatePath(startPiece);
                }
                else
                {
                    var Piece = CreatePieceForNewPath();
                    if(Piece != null)
                    { 
                        totalPieces.Add(Piece);
                        CreatePath(Piece);
                    }
                }
                currentPath++; 
                UIManager.Instance.LoadingBar.fillAmount = (currentPath / maxPathNumber) / 2;
                yield return null;
            }
            SetEndPiece();
            UIManager.Instance.LoadingBar.fillAmount = 0.75f;  

            AddElementsToPieces();
            DestroyPieces();
            GameManager.Instance.GetAllEnemies(); 
            UIManager.Instance.LoadingBar.fillAmount = 1f;
            AddWalls();
            foreach (var p in finalPathPieces)
                AddWalls(p.GetComponent<LevelPiece>());
            FinishedCreatingLevel = true;
            totalPieces[0].NavMeshSurface.BuildNavMesh();
            UIManager.Instance.LoadingScreen.SetActive(false);

            yield return null;
        }

        private void DestroyPieces()
        {
            foreach (var piece in piecesToDestroy)
                Destroy(piece);
        }

        void CreatePath(LevelPiece piece)
        {
            if(piece != null)
            { 
                InitializePiece(piece);
                CreateNewPieceIfNotReachedEnd();
            }
        } 
        LevelPiece CreatePieceForNewPath()
        {
            SetCurrentType();
            SetRemainingConnectorList(TotalRemainingConnectors);
            SetCurrentConnector(currentRemainingConnectors); 
            if(currentConnector != null)
                return SetPieceOnLevel(GetPiecePrefabFromList());  
            else 
                return null;
        } 
        void CreateNewPieceIfNotReachedEnd()
        {
            if (totalPiecesInCurrentPath < maxPiecesPerPath)
            { 
                SetCurrentConnector(currentPiece.Connectors);
                var newPiece = SetPieceOnLevel(GetPiecePrefabFromList());
                if (newPiece != null)
                    UsePiece(currentConnector, newPiece.GetComponent<LevelPiece>());
                else
                    ReachEnd(currentPiece);
            }
            else
            {
                ReachEnd(currentPiece);
            }
        } 
        void InitializePiece(LevelPiece piece)
        {
            if (piece == null)
                return;

            SetCurrentType();
            SetRemainingConnectorList(piece.Connectors);

            piece.name = $"Piece:{guid++}Path:{currentPath}";
            totalPiecesInCurrentPath++;
            currentPiece = piece;
        }
        void UsePiece(Transform usedConnector, LevelPiece usedPiece)
        {
            totalPieces.Add(usedPiece);
            currentRemainingConnectors.Remove(usedConnector);
            TotalRemainingConnectors.AddRange(currentRemainingConnectors);
            CreatePath(usedPiece);
        }
        LevelPiece SetPieceOnLevel(LevelPiece newPiecePrefab)
        {
            if (CheckCurrentPieceConnectors(newPiecePrefab))
                return InstantiatePiece(newPiecePrefab).GetComponent<LevelPiece>();
            else 
                return null; 
        }
        void ReachEnd(LevelPiece piece)
        {
            Debug.Log("Reached End");
            totalPieces.Remove(piece);
            piecesToDestroy.Add(piece.gameObject);
            finalPathPieces.Add(totalPieces[totalPieces.Count - 1].gameObject);
        }
        void SetEndPiece()
        {
            GetLongestDistance();
            ResetPosition(endPiece.transform);
        }
        bool CheckCurrentPieceConnectors(LevelPiece newPiecePrefab)
        { 
            var cont = 0; 
            var newPieceCollider = Instantiate(newPiecePrefab.Collider, currentConnector);

            while (CheckIfSpaceOccupied(newPieceCollider) && currentRemainingConnectors.Count > 0)
            {
                currentRemainingConnectors.Remove(currentConnector);

                if (currentRemainingConnectors.Count > 0)
                    SetCurrentConnector(currentRemainingConnectors);
                else
                    return false;

                ResetPosition(newPieceCollider.transform);
                cont++;
            }

            DiscardColliderAndAddRemainingConnectors(newPieceCollider);
            return true;
        }
        GameObject InstantiatePiece(LevelPiece newPiecePrefab)
            => Instantiate(newPiecePrefab.gameObject, currentConnector);
        void DiscardColliderAndAddRemainingConnectors(Collider newPieceCollider)
        {
            currentRemainingConnectors.Remove(currentConnector);

            if (piecesToDestroy == null)
                piecesToDestroy = new List<GameObject> { newPieceCollider.gameObject };
            else
                piecesToDestroy.Add(newPieceCollider.gameObject);

            TotalRemainingConnectors.AddRange(currentRemainingConnectors);
        }
        void AddElementsToPieces()
        {
            foreach (LevelPiece p in totalPieces) 
                AddEnemies(p);   
        }
        #endregion
    }



}
