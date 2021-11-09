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
        [SerializeField] GameObject projectile;
        [SerializeField] bool isMultipleProjectile = false;
        
        
        Animator anim; 
        #endregion

        #region Properties
        public Transform Objective {get; set;}
        public HealthScript PlayerHealthScript { get; set; }
        public Rigidbody PlayerRigidbody { get; set; }
        public float MeleeDistance { get; set; }
        public bool IsRanged { get => isRanged; }
        public Animator Anim { get => anim; set => anim = value; }
        #endregion

        #region Methods 
        void ShootProjectile()
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

        public void Melee()
        {
            if(Vector3.Distance(transform.position, Objective.transform.position) < MeleeDistance)
            {
                PlayerHealthScript.ForceRecieved = transform.forward * meleeAttackForce;
                PlayerHealthScript.ForceTypeReceived = ForceMode.Impulse;
                PlayerHealthScript.Damage(meleedamage);
            }
        }

        public void PerformMeleeAttack() => anim.SetTrigger("AttackMelee");
        public void PerformRagnedAttack() => anim.SetTrigger("AttackRange");

        public void Ranged()
            => ShootProjectile();
         
        #endregion
    }
}
