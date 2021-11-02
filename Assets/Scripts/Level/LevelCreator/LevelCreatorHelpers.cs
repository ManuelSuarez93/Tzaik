using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
using Tzaik.General;

namespace Tzaik.Level
{
    public partial class LevelCreator
    {
        #region Helpers
        LevelPiece GetRoomPiece()
            => currentList.Rooms[Random.Range(0, currentList.Rooms.Count)].LevelPiece;
        LevelPiece GetHallwayPiece()
            => currentList.Hallways[Random.Range(0, currentList.Hallways.Count)].LevelPiece;
        LevelPiece GetWallPiece()
            => currentList.Walls[Random.Range(0, currentList.Walls.Count)].LevelPiece;
        void GetLongestDistance()
        {
            var distances = new Dictionary<Transform, float>();
            foreach (Transform t in TotalRemainingConnectors)
            {
                if (!distances.ContainsKey(t))
                    distances.Add(t, Vector3.Distance(GameManager.Instance.playerPosition, t.position)); 
            }
            currentConnector = distances.OrderByDescending(x => x.Value).First().Key;

        }
        void SetCurrentConnector(List<Transform> connectorList) =>
            currentConnector = connectorList[Random.Range(0, connectorList.Count)];
        void SetRemainingConnectorList(List<Transform> connectorList)
            => currentRemainingConnectors = connectorList.GetRange(0, connectorList.Count);
        LevelPiece GetPiecePrefabFromList()
            => currentType == LevelPieceType.Room ? GetRoomPiece() : GetHallwayPiece();
        void ResetPosition(Transform transform)
        {
            transform.parent = currentConnector;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        bool PathsRemaining() => currentPath < maxPathNumber;
        void BakeNavMesh()
        { 
            foreach(LevelPiece p in totalPieces)
                p.NavMeshSurface.BuildNavMesh();
        }

        bool CheckIfSpaceOccupied(BoxCollider collider)
        {
            var total = totalPieces.Any(x => x.Collider.bounds.Intersects(collider.bounds));
            return total;
        }
        void AddWalls()
        { 
            var connectors = GameObject.FindGameObjectsWithTag("Connector");
            foreach (var connector in connectors)
                if (connector.transform.childCount == 0)
                    Instantiate(connector.GetComponentInParent<LevelPiece>().
                        Walls[Random.Range(0, connector.GetComponentInParent<LevelPiece>().Walls.Count)].LevelPiece.gameObject, 
                        connector.transform); 
        }

        void AddWalls(LevelPiece p)
        {
            foreach (var connector in p.Connectors) 
                Instantiate(p.Walls[Random.Range(0, p.Walls.Count)].LevelPiece.gameObject,
                    connector.transform);
        }
        void SetCurrentList()
            => currentList =
                currentArea == Area.Temple ? listTemple :
                currentArea == Area.CrystalCave ? listCave :
                currentArea == Area.Jungle ? listJungle : listTemple;
        void SetCurrentType() => currentType = currentType == LevelPieceType.Room ? LevelPieceType.Hallway : LevelPieceType.Room;
        void AddEnemies(LevelPiece p) => factory.SpawnItems(p.EnemySpawnPoints, Random.Range(0, maxEnemiesPerRoom + 1), factory.EnemyPool);

        void AddItems(LevelPiece p) => factory.SpawnItems(p.ItemSpawnPoints, Random.Range(0, maxItemsPerRoom + 1), factory.ItemPool);
        void AddCoins(LevelPiece p) => factory.SpawnItems(p.CoinSpawnPoints, Random.Range(0, maxCoinsPerRoom + 1), factory.CoinPool);
        void AddDecorations(LevelPiece p) => factory.SpawnItems(p.DecorationSpawnPoints, Random.Range(0, maxDecorationsPerRoom + 1), factory.DecorationPool);
        void AddTraps(LevelPiece p) => factory.SpawnItems(p.TrapSpawnPoints, Random.Range(0, maxTrapsPerRoom + 1), factory.TrapPool);

        #endregion
    }


}
