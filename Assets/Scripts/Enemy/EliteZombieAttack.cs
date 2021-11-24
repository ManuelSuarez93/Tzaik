using System.Collections.Generic;
using UnityEngine;

namespace Tzaik.Enemy
{
    public class EliteZombieAttack : EnemyAttack
    { 
        [Header("Summon settings")]
        [SerializeField] GameObject summonObject;
        [SerializeField] List<GameObject> summonedObjects;
        [SerializeField] float summonCooldown;
        [SerializeField] int maxSummons; 

        bool summonPerformed;
        float summonTimer;
        public int MaxSummons => maxSummons;

        protected override void Initialize()
        {
            base.Initialize();
            summonTimer = Time.time;
            summonPerformed = false;
        }
        protected override void Cooldowns()
        {
            base.Cooldowns();
            if (summonPerformed && summonTimer + summonCooldown <= Time.time)
            {
                summonPerformed = false;
                summonTimer = Time.time;
            }
        }
        protected override void ShootProjectile( ) 
        {
            GameObject o = Instantiate(projectile, Objective.position, Quaternion.identity);
            o.GetComponent<Animator>().SetTrigger("TrapTrigger");
        }
        public void Summon()
        {
            var summon = Instantiate(summonObject, transform.position + Vector3.right * 5, Quaternion.identity);
            summonedObjects.Add(summon);
        } 
        public void PerformSummon()
        {
            animator.SetTrigger("Summon");
            summonPerformed = true;
        }
         
    }
}
