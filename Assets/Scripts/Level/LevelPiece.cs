using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Tzaik.Level
{
    public class LevelPiece : MonoBehaviour
    {
        #region Fields
        [SerializeField] List<Transform> connectors;
        [SerializeField] List<Transform> enemySpawnPoints;
        [SerializeField] Collider collider;
        [SerializeField] List<LevelPieceObject> walls;
        [SerializeField] NavMeshSurface navMeshSurface;
        #endregion

        #region Properties
        public List<Transform> Connectors => connectors; 
        public List<Transform> EnemySpawnPoints => enemySpawnPoints;  
        public Collider Collider => collider; 
        public NavMeshSurface NavMeshSurface { get => navMeshSurface; set => navMeshSurface = value; }
        public List<LevelPieceObject> Walls  => walls;
        #endregion


    }

   
}
