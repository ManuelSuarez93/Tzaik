using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyAttackConditions : MonoBehaviour
    {
        [SerializeField] float meleeAttackCooldown;
        [SerializeField] float rangedAttackCooldown;
        [SerializeField] bool meleeAttackPerformed;
        [SerializeField] bool rangedAttackPerformed;

        float meleetimer;
        float rangedtimer;
        public bool MeleeAttackPerformed => meleeAttackPerformed; 
        public bool RangedAttackPerformed => rangedAttackPerformed;
        private void Start()
        {
            meleetimer = Time.time;
            rangedtimer = Time.time;
            meleeAttackPerformed = false;
            rangedAttackPerformed = false;
        }
        private void Update()
        {
            if (meleeAttackPerformed && meleetimer + meleeAttackCooldown <= Time.time)
            {
                meleeAttackPerformed = false;
                meleetimer = Time.time;
            }

            if (rangedAttackPerformed && rangedtimer + rangedAttackCooldown <= Time.time)
            {
                rangedAttackPerformed = false;
                rangedtimer = Time.time;
            }
        }

        public void PerformMelee() => meleeAttackPerformed = true; 
        public void PerformRange() => rangedAttackPerformed = true;
    }
}
