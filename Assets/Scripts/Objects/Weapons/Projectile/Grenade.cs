using System.Collections;
using System.Collections.Generic;
using Tzaik.General;
using Tzaik.Items.Misc;
using UnityEngine;

namespace Tzaik.Items
{
    public class Grenade : Projectile
    {
        [SerializeField] protected float sphereRadius; 

        //private void Start() 
        private void Update() => BulletTraversalByRigidbody();
        private void FixedUpdate() { }
        public override void DoAction(GameObject obj)
        {
            var o = Physics.OverlapSphere(transform.position, sphereRadius);
            foreach(Collider c in o)
            {
                var hs = GetHealthScript(c.gameObject);
                if (enemyTag.Contains(c.tag) && hs != null)
                    DoDamage(hs, Damage);
            }
             
        } 

        public override void InstantiateObject(int index) => Instantiate(instantiableObjects[index], transform.position, Quaternion.identity);
    }
}
