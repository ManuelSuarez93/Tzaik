using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Enemy
{
    public class EliteLizardAttack : EnemyAttack
    {
        [SerializeField] UnityEvent BlockingStart;
        [SerializeField] UnityEvent BlockingStop;
        bool isBlocking;

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
    }
}
