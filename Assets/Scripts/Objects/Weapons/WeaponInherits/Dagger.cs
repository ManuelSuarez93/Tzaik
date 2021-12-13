using Tzaik.Player;
using Tzaik.General;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Tzaik.Items.Weapons
{
    public class Dagger : Weapon
    {
        [Header("Dagger")]
        [SerializeField] float attackAreaSize;
        [SerializeField] List<string> damageTags;  
        public override void PerformAttack()
        { 
            if (AttackConidition)
            {
                animator.SetFloat("Attack", Random.Range(1, 3));
                animator.SetTrigger("AttackTrigger");
                attackEvent.Invoke();
            }
        }

        public override void AttackActionCheck()
        {
            var enemy = Physics.OverlapSphere(attack.ShootOrigin.position, attackAreaSize).ToList(); 
            if(enemy.First(x => damageTags.Contains(x.tag)) != null)
                hitEvent.Invoke();
            foreach (var e in enemy)
                if (damageTags.Contains(e.tag))
                    e.GetComponent<HealthScript>().Damage(attack.BaseDamage);

            rate = 0;  
        }
         
        protected override bool AttackConidition => rate == shootRate;
    }
}
