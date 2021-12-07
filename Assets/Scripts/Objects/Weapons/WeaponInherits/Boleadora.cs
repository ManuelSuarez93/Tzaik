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
    }
     
}
