using Tzaik.General;
using System.Collections.Generic;
using UnityEngine;

namespace Tzaik.Level
{
    public class PieceFactory : MonoBehaviour
    {
        #region Fields
        [SerializeField] PrefabPool enemyPool;
        [SerializeField] PrefabPool decorationPool;
        
        #endregion

        #region Properties 
        public PrefabPool EnemyPool => enemyPool; 
        public PrefabPool DecorationPool => decorationPool; 
        #endregion  

        #region Methods
        public void SpawnItems(List<Transform> spawnPoints, int amount, PrefabPool pool)
        {
            if (pool.HasAvailableObjects && spawnPoints.Count > 0)
            {
                int j;
                for (int i = 0; i < amount; i++)
                {
                    var cont = 0;
                    j = Random.Range(0, spawnPoints.Count);
                    while (TransformHasChild(spawnPoints[j]) && cont < spawnPoints.Count)
                    {
                        cont++;
                        j = Random.Range(0, spawnPoints.Count);
                    }

                    pool.InstanceObject(spawnPoints[j].position);
                }
            }
        }
        public bool TransformHasChild(Transform t) => t.childCount > 0;
        #endregion
    }
}
