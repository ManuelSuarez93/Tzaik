using RootMotion.Dynamics;
using RootMotion.FinalIK;
using System;
using System.Collections;
using System.Collections.Generic;
using Tzaik.General;
using Tzaik.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Tzaik.Enemy
{
    public enum EnemyType
    {
        Pigmy = 0, Zombie = 1, Lizard = 2, Leech = 3
    }

    [RequireComponent(typeof(EnemyAgent), typeof(EnemyDetect))] 
    [Serializable]
    public class EnemyContext : Spawnable
    {
        #region Fields
        [SerializeField] bool enableDebug;
        [SerializeField] Renderer mesh;
        [SerializeField] GameObject sounds;
        [SerializeField] bool doBehaviorTree; 
        [SerializeField] UnityEvent stunEvent;
        [SerializeField] UnityEvent stunExitEvent;
        [SerializeField] LookAtIK lookAtIk;

        public Blackboard blackboard = new Blackboard();
        EnemyDetect detect; 
        EnemyAgent agent;
        EnemyAttack attack; 
        HealthScript health;
        EnemyAnimator enemyAnimator; 
        #endregion

        #region Properties 
        public EnemyAgent Agent => agent; 
        public EnemyDetect Detect => detect;
        public EnemyAttack Attack => attack; 
        public HealthScript Health  => health;   

        #endregion

        #region Unity Methods
        void Awake()
        {
            agent = GetComponent<EnemyAgent>(); 
            detect = GetComponent<EnemyDetect>(); 
            attack = GetComponent<EnemyAttack>();
            health = GetComponent<HealthScript>();
            enemyAnimator = GetComponent<EnemyAnimator>(); 
            enemyAnimator.SetAnimator(GetComponentInChildren<Animator>());

            attack.PlayerRigidbody = GameManager.Instance.Player.GetComponent<Rigidbody>();
            attack.PlayerHealthScript = GameManager.Instance.Player.GetComponent<PlayerController>().Health;
            if (lookAtIk != null)
                lookAtIk.solver.target = GameManager.Instance.Player.transform;
            if (enemyAnimator != null)
                attack.SetAnimator(enemyAnimator);
            if(detect != null)
                attack.MeleeDistance = detect.MeleeDistance;
        }
          
        void Update() => SetContext();
        private void LateUpdate()
        {
            if (detect.playerTransform != null)
                transform.forward = detect.playerTransform.position - transform.position;
        }
        #endregion

        #region Methods 
        public void SetStunned()
        { 
            if(health.Damaged)
            {
                enemyAnimator.SetTrigger("Stuned");
                StartCoroutine(StunnedCoroutine(health.StunTime)); 
                health.Damaged = false;
            }
        }

        IEnumerator StunnedCoroutine(float stuntime)
        {
            float timer = 0;
            while(timer < stuntime)
            {
                blackboard.isStunned = true;
                agent.NavAgent.SetDestination(transform.position);
                stunEvent.Invoke();
                timer += Time.deltaTime;
                yield return null;
            }
            stunExitEvent.Invoke();
            blackboard.isStunned = false;
        }
        void SetContext()
        {
            enemyAnimator.Animations(agent);
            blackboard.CurrentDetectState = detect.CurrentState;
            blackboard.CanMelee = !attack.MeleeAttackPerformed;
            blackboard.CanRanged = !attack.RangedAttackPerformed;
            blackboard.CurrentHealth = health.CurrentHealth;
            blackboard.CurrentPosition = transform.position;
            blackboard.isStunned = health.Damaged;
            blackboard.NextPosition = detect.playerTransform != null ? detect.playerTransform.position : blackboard.NextPosition;
            //if (detect.playerTransform != null)
            //    detect.LookAtPlayer();
            Attack.Objective = detect.playerTransform != null ? detect.playerTransform : null;
            blackboard.Context = this;
        }
         
        #endregion
    }
}
