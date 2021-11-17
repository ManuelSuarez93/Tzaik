using RootMotion.Dynamics;
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
        [SerializeField] Animator animator;
        [SerializeField] UnityEvent stunEvent;
        [SerializeField] UnityEvent stunExitEvent;

        public Blackboard blackboard = new Blackboard();
        EnemyDetect detect; 
        EnemyAgent agent;
        EnemyAttack attack; 
        HealthScript health;
        EnemyAttackConditions attackConditions;
        #endregion

        #region Properties 
        public EnemyAgent Agent => agent; 
        public EnemyDetect Detect => detect;
        public EnemyAttack Attack => attack; 
        public HealthScript Health  => health; 
        public Animator Animator { get => animator; set => animator = value; } 
        public EnemyAttackConditions AttackConditions => attackConditions;

        #endregion

        #region Unity Methods
        void Awake()
        {
            agent = GetComponent<EnemyAgent>();
            detect = GetComponent<EnemyDetect>(); 
            attack = GetComponent<EnemyAttack>();
            attackConditions = GetComponent<EnemyAttackConditions>();
            if(animator == null)
                animator = GetComponentInChildren<Animator>();
            attack.Anim = animator;
            attack.MeleeDistance = detect.MeleeDistance;
            health = GetComponent<HealthScript>(); 
        }

        private void Start()
        {
            attack.PlayerRigidbody = GameManager.Instance.Player.GetComponent<Rigidbody>();
            attack.PlayerHealthScript = GameManager.Instance.Player.GetComponent<PlayerController>().Health; 
        }

        void Update() => SetContext();
        public void SetStunned()
        { 
            if(health.Damaged)
            { 
                Animator.SetTrigger("Stunned");
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
            Animator.SetFloat("SpeedX", agent.ForwardVelocity);
            Animator.SetFloat("SpeedY", Mathf.InverseLerp(-0.99f, 1, agent.RightVelocity));
            Animator.SetFloat("SpeedYLeft", Mathf.InverseLerp(-0.99f, 1, agent.LeftVelocity));
            Animator.SetFloat("Rotation", agent.CalculateForward());
            blackboard.CurrentDetectState = detect.CurrentState;
            blackboard.CanMelee = !attackConditions.MeleeAttackPerformed; 
            blackboard.CanRanged = !attackConditions.RangedAttackPerformed;
            blackboard.CurrentHealth = health.CurrentHealth;
            blackboard.CurrentPosition = transform.position;
            blackboard.isStunned = health.Damaged;
            blackboard.NextPosition = detect.playerTransform != null ? detect.playerTransform.position : blackboard.NextPosition;
            if (detect.playerTransform != null)
                detect.LookAtPlayer();
            Attack.Objective = detect.playerTransform != null ? detect.playerTransform : null;
            blackboard.Context = this;
        }
         
         
        #endregion

        #region Methods 
        #endregion
    }
}
