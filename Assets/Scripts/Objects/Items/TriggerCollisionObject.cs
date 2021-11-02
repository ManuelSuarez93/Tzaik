using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

namespace Tzaik.Items.Misc
{
    public abstract class TriggerCollisionObject : MonoBehaviour
    { 
        [Header("Trigger/Collision properties")]
        [SerializeField] protected List<string> tags;
        [SerializeField] protected bool destroyOnCollision;
        [SerializeField] protected bool checkDoActionReturnsBool;
        [SerializeField] protected UnityEvent collisionEnterEvent;
        [SerializeField] protected UnityEvent destroyEvent;
        [SerializeField] protected List<GameObject> instantiableObjects; 
        protected Vector3 collisionPoint;

        void OnTriggerEnter(Collider other)
        {
            collisionPoint = other.bounds.extents;
            GetCollisionObjectAndCheckDoAction(other.gameObject); 
        }
        void OnCollisionEnter(Collision collision)
        {
            SetCollisionPoint(collision.contacts.FirstOrDefault().point);
            GetCollisionObjectAndCheckDoAction(collision.gameObject);
        }
        protected virtual void GetCollisionObjectAndCheckDoAction(GameObject collision)
        {
            if (CompareTagsWithList(collision.tag))
            {
                if (checkDoActionReturnsBool)
                {
                    if (DoActionReturnBool(collision.gameObject))
                        collisionEnterEvent.Invoke();
                } 
                else
                { 
                    DoAction(collision); 
                    collisionEnterEvent.Invoke();
                } 

                if (destroyOnCollision)
                {
                    destroyEvent.Invoke();
                    Destroy(gameObject);
                }
            }
        }
        public virtual void SetCollisionPoint(Vector3 point) => collisionPoint = point;
        public virtual bool CompareTagsWithList(string collisionTag) => tags.Contains(collisionTag); 
        public virtual bool DoActionReturnBool(GameObject obj = null) { return false; }
        public virtual void DoAction(GameObject obj) {  }
        public virtual void InstantiateObject(int index) => Instantiate(instantiableObjects[index], collisionPoint, Quaternion.identity);
    }
   
}
