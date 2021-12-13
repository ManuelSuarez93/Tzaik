using RootMotion.Dynamics;
using System.Collections.Generic;
using Tzaik.Enemy;
using Tzaik.General;
using Tzaik.Player;
using UnityEngine;
using System.Linq;
using Tzaik.Items.Weapons;
using UnityEngine.Events;

namespace Tzaik.Items
{
    public class Spear : Projectile
    {
        [SerializeField] int PassThroughEnemiesAmount;
        [SerializeField] GameObject point; 
        [SerializeField] protected UnityEvent spearCollisionEvent;
        public List<EnemyContext> enemies;
        public List<Joint> joints; 
        int currentPassThrough;

        public override void DoAction(GameObject obj)
        {
            if(!isStopped)
            { 
                if (currentPassThrough < PassThroughEnemiesAmount)
                {
                    if (enemyTag.Contains(obj.tag))
                    {
                        if (obj.GetComponent<HealthScript>() != null)
                            obj.GetComponent<HealthScript>().Damage(Damage);
                        AddJointToSpear(obj);;
                    }
                    else if(CompareTagsWithList(obj.tag))
                    {
                        currentPassThrough = PassThroughEnemiesAmount;
                        isStopped = true;
                    }
                    spearCollisionEvent.Invoke();
                }
                else
                { 
                    isStopped = true;
                }
            }
            else
            {
                if(obj.CompareTag("Player"))
                {
                    var atlatl = GameObject.FindObjectsOfType<Weapon>().FirstOrDefault(x => x.Type == WeaponType.Atlatl);
                    if(atlatl != null)
                    {
                        atlatl.WeaponAmmo.AddAmmo(1);
                        Destroy(gameObject);
                    }
                         
                }
            }
        }

        void AddJointToSpear(GameObject obj)
        {
            if (enemies.Contains(obj.GetComponentInParent<EnemyContext>())) return;

            var puppet = obj.GetComponentInParent<PuppetMaster>();
            puppet.mode = PuppetMaster.Mode.Active;
            puppet.pinWeight = 0;
            var joint = point.AddComponent<CharacterJoint>();
            joint.connectedBody = obj.GetComponent<Rigidbody>(); 
            enemies.Add(obj.GetComponentInParent<EnemyContext>()); 
            currentPassThrough++;
        }
 
         
    }

}
