using Tzaik.General;
using Tzaik.Player;
using UnityEngine;

namespace Tzaik.Items.Weapons
{
    public class BlowGun : Weapon
    { 
        public override void PerformAttack()
        { 
            if (AttackConidition)
            {
                animator.SetTrigger("Attack");
                Attack();
            }
        }
    }
     
}
