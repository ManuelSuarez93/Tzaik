using Tzaik.Player;
using Tzaik.General;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using RootMotion.Dynamics;

namespace Tzaik.Items.Weapons
{
    public class Macahuitl : Weapon
    {
        [Header("Macahuitl")]
        [SerializeField] float attackAreaSize;
        [SerializeField] List<string> damageTags;  

        [Header("Macahuitl Special")]
        [SerializeField] float waveSpeed;
        [SerializeField] float waveDamage;
        [SerializeField] Projectile wave; 
        public override void PerformAttack()
        {
            if (AttackConidition)
            {  
                animator.SetFloat("Attack", Random.Range(1, 3));
                animator.SetTrigger("AttackTrigger"); 
                rate = 0;
            }
        }
        public override void AttackActionCheck()
        {
            var enemy = Physics.OverlapSphere(attack.ShootOrigin.position, attackAreaSize);
            foreach (var e in enemy)
                if (damageTags.Contains(e.tag))
                { 
                    e.GetComponent<HealthScript>().Damage(attack.BaseDamage + attack.AdditionalDamage);  
                    e.GetComponent<Rigidbody>().AddForce(transform.forward * (attack.BaseForce + attack.AdditionalForce), ForceMode.Impulse);
                }

            attackEvent.Invoke();
        } 

        public void InstantiateWave()
        {
            var newWave = Instantiate(wave, transform);
            newWave.transform.forward = transform.parent.forward;
            newWave.transform.parent = null;
            newWave.Speed = waveSpeed;
            newWave.Damage = waveDamage;
        }

        protected override bool AttackConidition => rate == shootRate;
    }
}
