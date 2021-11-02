using Tzaik.General;
using Tzaik.Player;
using UnityEngine;

namespace Tzaik.Items.Weapons
{
    public class BlowGun : Weapon
    {
        public override void PerformSpecial(PlayerSpecial special)
        {
            if (special.CurrentSpecial >= specialCost)
            {
                animator.SetTrigger("Attack");
                attack.InstantiateProjectile(attack.SpecialProjectile);
                special.CurrentSpecial -= specialCost;
            }
        }
    }
}
