using System.Collections.Generic;
using Tzaik.Enemy;
using Tzaik.General;
using UnityEngine;

namespace Tzaik.Items
{
    public class Spear : Projectile
    {
        [SerializeField] int PassThroughEnemiesAmount;
        [SerializeField] GameObject point; 
        public List<EnemyContext> enemies;
        public List<Joint> joints;
        int currentPassThrough;

        public override void DoAction(GameObject obj)
        {
            if (currentPassThrough < PassThroughEnemiesAmount)
            {
                if (enemyTag.Contains(obj.tag))
                {
                    if (obj.GetComponent<RecieveDamage>() != null)
                        obj.GetComponent<RecieveDamage>().Damage(Damage);
                    AddJointToSpear(obj);
                    collisionEnterEvent.Invoke();
                }
                else if(CompareTagsWithList(obj.tag))
                {
                    currentPassThrough = PassThroughEnemiesAmount;
                    isStopped = true;
                }
            } 
        }

        void AddJointToSpear(GameObject obj)
        {
            if (enemies.Contains(obj.GetComponentInParent<EnemyContext>())) return;

            var joint = point.AddComponent<CharacterJoint>();
            joint.connectedBody = obj.GetComponent<Rigidbody>(); 
            enemies.Add(obj.GetComponentInParent<EnemyContext>()); 
            currentPassThrough++;
        }
         
    }

}
