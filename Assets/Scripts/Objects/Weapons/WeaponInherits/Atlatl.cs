using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tzaik.Items.Weapons
{
    public class Atlatl : Weapon
    {
        public override void PerformAttack()
        { 
            if (AttackConidition)
                animator.SetTrigger("Attack");
        }
         
    }
}
