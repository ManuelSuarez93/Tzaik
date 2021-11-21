using System.Collections;
using System.Collections.Generic;
using Tzaik.Enemy;
using Tzaik.General;
using UnityEngine;

namespace Tzaik
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] float rate;
        [SerializeField] PrefabPool enemyPool;
        [SerializeField] List<Transform> positions;
        [SerializeField] Transform playerTransform;
        float timer;
        void Start()
        { 
            timer = Time.time;
        }
         
        void Update()
        {
            if (timer + rate <= Time.time)
            {
                var enemy = enemyPool.InstanceObject(positions[Random.Range(0, positions.Count - 1)].position);
                if (enemy != null)
                    enemy.GetComponent<EnemyContext>().Agent.NavAgent.SetDestination(playerTransform.position);
                timer = Time.time;
            }     
        }
    }
}
