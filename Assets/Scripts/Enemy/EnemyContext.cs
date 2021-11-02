using RootMotion.Dynamics;
using System;
using System.Collections;
using System.Collections.Generic;
using Tzaik.General;
using Tzaik.Player;
using UnityEngine;

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

        public Blackboard blackboard = new Blackboard();
        EnemyDetect detect;
        EnemyState currentState;
        EnemyAgent agent;
        EnemyAttack attack; 
        HealthScript health;
        Animator animator; 
        #endregion

        #region Properties
        public EnemyState CurrentState { get => currentState; set => currentState = value; }
        public EnemyAgent Agent { get => agent; }
        public EnemyDetect Detect { get => detect;}
        public EnemyAttack Attack { get => attack; }
        public HealthScript Health { get => health; }
        public Renderer Mesh { get => mesh; set => mesh = value; }
        public Animator Animator { get => animator; set => animator = value; }
        public GameObject Sounds { get => sounds; set => sounds = value; }

        #endregion

        #region Unity Methods
        void Awake()
        {
            agent = GetComponent<EnemyAgent>();
            detect = GetComponent<EnemyDetect>();
            detect.Agent = agent;
            attack = GetComponent<EnemyAttack>();
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
                agent.SetDestination(transform.position);
                timer += Time.deltaTime;
                yield return null;
            }
            blackboard.isStunned = false;
        }
        void SetContext()
        {
            Animator.SetFloat("SpeedX", agent.ForwardVelocity);
            Animator.SetFloat("SpeedY",  agent.RightVelocity);
            Animator.SetFloat("SpeedYLeft", Mathf.InverseLerp(-0.99f, 1, agent.LeftVelocity));
            Animator.SetFloat("Rotation", agent.CalculateForward());
            blackboard.CurrentDetectState = detect.CurrentState;
            blackboard.CurrentHealth = health.CurrentHealth;
            blackboard.CurrentPosition = transform.position;
            blackboard.isStunned = health.Damaged;
            blackboard.NextPosition = detect.playerTransform != null ? detect.playerTransform.position : blackboard.NextPosition;
            if (detect.playerTransform != null)
                transform.LookAt(detect.playerTransform);
            Attack.Objective = detect.playerTransform != null ? detect.playerTransform : null;
            blackboard.Context = this;
        }
         
         
        #endregion

        #region Methods 
        #endregion
    }
}
