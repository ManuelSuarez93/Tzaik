using System;
using System.Collections.Generic;
using Tzaik.Enemy;
using UnityEngine;

namespace Tzaik.Items
{
    public class SlingStoneRicochet : Projectile
    {
        [Header("Ricochet Settings")]
        [SerializeField] int maxBounces;
        [SerializeField] float ricochetAngleAmount;
        [SerializeField] List<GameObject> objectsCollided; 
        int currentBounces;
         
        public override void DoAction(GameObject obj)
        {  
            var hs = GetHealthScript(obj);
            if (hs != null && enemyTag.Contains(obj.tag))
                DoDamage(hs, Damage);

            transform.localEulerAngles *= (Vector3.Angle(transform.forward, collisionPoint) + ricochetAngleAmount);
        }
    }


}
