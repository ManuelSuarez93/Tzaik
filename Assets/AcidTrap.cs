using System.Collections;
using System.Collections.Generic;
using Tzaik.Items;
using UnityEngine;

namespace Tzaik
{
    public class AcidTrap : MonoBehaviour
    {
        [SerializeField] float trapCooldown, damage, force, speed;
        [SerializeField] Transform shootOrigin;
        [SerializeField] Projectile projectile;
        [SerializeField] string triggerName;
        float traptimer;
        void Start() => traptimer = Time.time;
        

        public void SetTrigger()
        {
            if (traptimer + trapCooldown <= Time.time)
            {
                InstantiateProjectile();
                traptimer = Time.time;
            }
        }

        public void InstantiateProjectile()
        {
            GameObject o = GameObject.Instantiate(projectile.gameObject, shootOrigin);
            o.transform.parent = null;
            o.GetComponent<Projectile>().Damage = damage;
            o.GetComponent<Projectile>().Speed = speed;
            o.GetComponent<Projectile>().ForceImpactAmount = force; 
        }
         
    }
}
