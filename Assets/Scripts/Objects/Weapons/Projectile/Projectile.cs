using UnityEngine;
using Tzaik.General;
using Tzaik.Items.Misc;
using Tzaik.Audio;
using System.Collections.Generic;

namespace Tzaik.Items
{
    public class Projectile : TriggerCollisionObject
    {
        #region Fields 
        [Header("Projectile properties")]
        [SerializeField] protected bool isStopped = false; 
        [SerializeField] protected bool precisionDamage = false;
        [SerializeField] protected List<string> enemyTag;
        [SerializeField] protected bool moveByRigidobdy = false;
        [SerializeField] protected Rigidbody rigid;
        [SerializeField] protected float forceImpactAmount, gravityApplied, slowDownRate;
        
        #endregion

        #region Properties
        public float Speed { protected get; set; }
        public float Damage { protected get; set; }
        public float ForceImpactAmount { protected get; set; }
        RaycastHit hit; 
        #endregion

        #region Unity Methods  
        private void FixedUpdate()
        {
            if (!moveByRigidobdy)
                BulletTraversalByRaycast();

            if (moveByRigidobdy)
                BulletTraversalByRigidbody();
        }
        #endregion

        #region Methods
        protected virtual void BulletTraversalByRaycast()
        {
//RaycastHit(transform.forward) ? hit.point : 
            if(!isStopped) 
                SetTransformPosition(transform,  transform.position + (transform.forward * GetDistance() + (Vector3.down * gravityApplied)));

            if (transform.position == hit.point)
            {
                SetCollisionPoint(hit.point);
                GetCollisionObjectAndCheckDoAction(hit.collider.gameObject);

                if(destroyOnCollision)
                    Destroy(gameObject);
            }
        }
        protected virtual void BulletTraversalByRigidbody()
        {
            rigid.MovePosition(transform.position + (Speed * transform.forward * Time.fixedDeltaTime));
            if (Speed > 0)
                Speed--;
        }

        protected void SetTransformPosition(Transform t,Vector3 position) 
            => t.position = position;
        protected bool RaycastHit(Vector3 forward) 
            => Physics.Raycast(transform.position, forward, out hit, GetDistance(), Physics.AllLayers, QueryTriggerInteraction.Ignore);
        protected float GetDistance() => (Speed - Time.fixedDeltaTime * slowDownRate) * Time.fixedDeltaTime;
        protected HealthScript GetHealthScript(GameObject o)
            => o.GetComponent<HealthScript>();
        protected HealthScript GetHealthScriptInParent(GameObject o)
            => o.GetComponentInParent<HealthScript>();
        protected void DoDamage(HealthScript hs, float damage)
            => hs.Damage(damage);
        public override void DoAction(GameObject obj)
        {
            HealthScript hs;
            if (precisionDamage)
                hs = GetHealthScriptInParent(obj);
            else
                hs = GetHealthScript(obj);

            if (hs != null && enemyTag.Contains(obj.tag))
                DoDamage(hs, Damage);
            var rb = obj.GetComponent<Rigidbody>();

            if (rb) 
                AddForceToRigid(obj.GetComponent<Rigidbody>(), transform.forward);

            if (destroyOnCollision)
                Destroy(gameObject);
        }

        public virtual void AddForceToRigid(Rigidbody rb, Vector3 direction) 
            => rb.AddForce(direction * forceImpactAmount, ForceMode.Impulse);
        #endregion
    }


}
