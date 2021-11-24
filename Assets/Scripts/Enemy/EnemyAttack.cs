using Tzaik.General;
using Tzaik.Items;
using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        #region Fields
        [SerializeField] float meleedamage;
        [SerializeField] float rangedDamage;
        [SerializeField] float attackRate;
        [SerializeField] float meleeAttackForce;
        [Header("RangedAttack")]
        [SerializeField] bool isRanged;
        [SerializeField] float speed;
        [SerializeField] Transform shootPoint;
        [SerializeField] protected GameObject projectile;
        [SerializeField] bool isMultipleProjectile = false;
        [Header("Attack conditions")]
        [SerializeField] float meleeAttackCooldown;
        [SerializeField] float rangedAttackCooldown;
        [SerializeField] bool meleeAttackPerformed;
        [SerializeField] bool rangedAttackPerformed;

        float meleeTimer;
        float rangedTimer;
        public bool MeleeAttackPerformed => meleeAttackPerformed;
        public bool RangedAttackPerformed => rangedAttackPerformed;  

        protected EnemyAnimator animator; 
        #endregion

        #region Properties
        public Transform Objective {get; set;}
        public HealthScript PlayerHealthScript { get; set; }
        public Rigidbody PlayerRigidbody { get; set; }
        public float MeleeDistance { get; set; }
        public bool IsRanged { get => isRanged; }
        #endregion
        #region UnityMethods
        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            meleeTimer = Time.time;
            rangedTimer = Time.time;
            meleeAttackPerformed = false;
            rangedAttackPerformed = false;
        }

        private void Update()
        {
            Cooldowns();
        }

        protected virtual void Cooldowns()
        {
            if (meleeAttackPerformed && meleeTimer + meleeAttackCooldown <= Time.time)
            {
                meleeAttackPerformed = false;
                meleeTimer = Time.time;
            }

            if (rangedAttackPerformed && rangedTimer + rangedAttackCooldown <= Time.time)
            {
                rangedAttackPerformed = false;
                rangedTimer = Time.time;
            }
        }
        #endregion
        #region Methods 
        protected virtual void ShootProjectile()
        {
            GameObject o = Instantiate(projectile, shootPoint);
            o.transform.forward = Objective.position;
            o.transform.LookAt(Objective.position + (Vector3.up * 4));
            o.transform.parent = null;
            if(isMultipleProjectile)
            { 
                if (o.transform.childCount > 0)
                {
                    for (int i = 0; i < o.transform.childCount; i++)
                    {
                        o.transform.GetChild(i).GetComponent<Projectile>().Damage = rangedDamage;
                        o.transform.GetChild(i).GetComponent<Projectile>().Speed = speed;
                    }
                }
            }
            else
            { 
                o.GetComponent<Projectile>().Damage = rangedDamage;
                o.GetComponent<Projectile>().Speed = speed;
            }
        }

        public virtual void Melee()
        {
            if(Vector3.Distance(transform.position, Objective.transform.position) < MeleeDistance)
            {
                PlayerHealthScript.ForceRecieved = (transform.forward * meleeAttackForce);
                PlayerHealthScript.ForceTypeReceived = ForceMode.Impulse;
                PlayerHealthScript.Damage(meleedamage);
            }
        }
        
        public void SetAnimator(EnemyAnimator a) => animator  = animator ??= a;
        public void PerformMeleeAttack()
        {
            animator.SetTrigger("AttackMelee"); 
            meleeAttackPerformed = true;
        }

        public void PerformRagnedAttack()
        {
            animator.SetTrigger("AttackRange");
            rangedAttackPerformed = true;
        }

        public void Ranged()
            => ShootProjectile();
         
        #endregion
    }
}
