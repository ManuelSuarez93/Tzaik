using RootMotion.Dynamics;
using System;
using UnityEngine;

namespace Tzaik.Enemy
{
    [Serializable]
    public class Blackboard
    {
        [SerializeField] EnemyDetect.DetectState currentDetectState;
        [SerializeField] float currentHealth;
        [SerializeField] Vector3 currentPosition;
        [SerializeField] Vector3 nextPosition;
        public bool CanMelee;
        public bool CanRanged;
        public bool isStunned; 
        public EnemyContext Context;

        public EnemyDetect.DetectState CurrentDetectState { get => currentDetectState; set => currentDetectState = value; }
        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public Vector3 CurrentPosition { get => currentPosition; set => currentPosition = value; }
        public Vector3 NextPosition { get => nextPosition; set => nextPosition = value; }
    }
}
