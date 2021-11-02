using Tzaik.Player;
using Tzaik.General;
using UnityEngine;
using System.Collections.Generic;

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
                animator.SetTrigger("Attack");
        }
        public override void AttackActionCheck()
        {
            var enemy = Physics.OverlapSphere(attack.ShootOrigin.position, attackAreaSize);
            foreach (var e in enemy)
                if (damageTags.Contains(e.tag))
                    e.GetComponent<HealthScript>().Damage(attack.BaseDamage);

            attackEvent.Invoke();
            rate = 0;
        }
         

        protected override bool AttackConidition => rate == shootRate;
    }
}
