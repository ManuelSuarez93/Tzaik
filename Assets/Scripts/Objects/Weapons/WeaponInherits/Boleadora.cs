using Tzaik.Player;

namespace Tzaik.Items.Weapons
{
    public class Boleadora : Weapon
    {
        public override void PerformAttack()
        {
            if (AttackConidition)
            { 
                animator.SetTrigger("AttackTrigger");
                rate = 0;
            }
        }

        public override void PerformSpecial(PlayerSpecial special)
        {
            if (special.CurrentSpecial >= specialCost && specialRate >= specialTime)
            {
                animator.SetTrigger("SpecialTrigger");
                special.CurrentSpecial -= specialCost;
                specialRate = 0;
            }
        }

        public override void DoSpecial() => attack.InstantiateProjectile(attack.SpecialProjectile);
    }
     
}
