using Tzaik.Enemy;
using UnityEngine;

namespace Tzaik.Items
{
    public class GrenadeSpecial : Grenade
    {
        [SerializeField] LayerMask layers;
        public override void DoAction(GameObject obj)
        {
            var o = Physics.OverlapSphere(transform.position, sphereRadius, layers);
            foreach (Collider c in o)
            {
                var rigid = c.GetComponent<Rigidbody>();
                rigid.MovePosition(collisionPoint); 
                var hs = GetHealthScript(c.gameObject);
                if (enemyTag.Contains(c.tag) && hs != null)
                    DoDamage(hs, Damage);
            }
            Destroy(this);
        }
         
    }
}
