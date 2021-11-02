using System.Collections;
using System.Collections.Generic;
using Tzaik.General;
using Tzaik.Items.Misc;
using UnityEngine;

namespace Tzaik.Enemy
{
    public class LeechPoison : TriggerCollisionObject
    {
        [SerializeField] float damage;
        public override void DoAction(GameObject obj)
        {
            if (tags.Contains(obj.tag))
             obj.GetComponent<HealthScript>().Damage(damage);
        }
    }
}
