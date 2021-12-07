using Tzaik.Items;
using UnityEngine;
using Tzaik.General;

namespace Tzaik.Enemy
{
    public class FinalBossAttack : MonoBehaviour
    { 
        [SerializeField] float rangedDamage;
        [SerializeField] float attackRate;
        [SerializeField] float speed;
        [SerializeField] GameObject projectile;
        [SerializeField] Transform shootPoint;
        Transform objective;
        float timer;
        void Start()
        {
            timer = Time.time;
            objective = GameManager.Instance.Player.transform;
        }
        private void Update()
        {
            Cooldowns();
        }

        protected virtual void Cooldowns()
        { 
            if (timer + attackRate <= Time.time)
            { 
                ShootProjectile();
                timer = Time.time;
            }
        }

          protected virtual void ShootProjectile()
        {
            GameObject o = Instantiate(projectile, shootPoint);
            o.transform.forward = objective.position;
            o.transform.LookAt(objective.position + (Vector3.up * 4));
            o.transform.parent = null; 
            o.GetComponent<Projectile>().Damage = rangedDamage;
            o.GetComponent<Projectile>().Speed = speed; 
        }
    }
}
