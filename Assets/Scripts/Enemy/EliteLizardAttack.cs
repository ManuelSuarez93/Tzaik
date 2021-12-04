using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Enemy
{
    public class EliteLizardAttack : EnemyAttack
    {
        #region Fields
        [SerializeField] UnityEvent BlockingStart;
        [SerializeField] UnityEvent BlockingStop;
        [SerializeField] float blockingCooldown;
        [SerializeField] bool blockingPerformed;
        float blockingTimer;
        bool isBlocking; 
        #endregion
        #region Properties
        public bool BlockingPerformed => blockingPerformed;
        #endregion

        #region Class methods
        protected override void Cooldowns()
        {
            base.Cooldowns();
            if (blockingPerformed && blockingTimer + blockingCooldown <= Time.time)
            {
                blockingPerformed = false;
                blockingTimer = Time.time;
            }
        }
        public bool IsBlocking => isBlocking;
        public void Startblocking(bool start)
        {
            isBlocking = start;
            animator.SetBool("IsBlocking", isBlocking);
            if (start)
                BlockingStart.Invoke();
            else
                BlockingStop.Invoke();
        }

        public void PerformBlock(bool start)
        { 
            Startblocking(start);
            blockingPerformed = true;
        }

        #endregion

        
    }
}
