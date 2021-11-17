using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyAttackConditions: MonoBehaviour
    { 
        [SerializeField] float meleeAttackCooldown;
        [SerializeField] float rangedAttackCooldown;
        [SerializeField] bool meleeAttackPerformed;
        [SerializeField] bool rangedAttackPerformed;

        float timer;
        public bool MeleeAttackPerformed => meleeAttackPerformed; 
        public bool RangedAttackPerformed => rangedAttackPerformed;
        private void Start()
        {
            timer = Time.time;
            meleeAttackPerformed = false;
            rangedAttackPerformed = false;
        }
        private void Update()
        {
            if (meleeAttackPerformed && timer + meleeAttackCooldown <= Time.time)
            {
                meleeAttackPerformed = false;
                timer = Time.time;
            }

            if (rangedAttackPerformed && timer + rangedAttackCooldown >= Time.time)
            {
                rangedAttackPerformed = false;
                timer = Time.time;
            }
        }

        public void PerformMelee() => meleeAttackPerformed = true; 
        public void PerformRange() => rangedAttackPerformed = true;
    }
}
