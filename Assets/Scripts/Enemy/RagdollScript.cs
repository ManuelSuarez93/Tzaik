using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tzaik
{
    public class RagdollScript : MonoBehaviour
    {
        #region Fields
        Rigidbody[] rigids;
        Collider[] colliders;
        Joint[] joints;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            rigids = GetComponentsInChildren<Rigidbody>();
            colliders = GetComponentsInChildren<Collider>();
            joints = GetComponentsInChildren<Joint>();

            DisableRagdoll();
        }

        private void Update()
        {
            foreach(Rigidbody r in rigids)
            {
                r.velocity = new Vector3(Mathf.Clamp(r.velocity.x, -1, 1), Mathf.Clamp(r.velocity.y, -1, 1), Mathf.Clamp(r.velocity.y, -1, 1)); 
            }
        }
        #endregion

        #region Methods
        public void EnableRagdoll()
        {
            foreach(Rigidbody r in rigids)
            {
                r.isKinematic = false;
                r.velocity = Vector3.zero;
            }
        }

        public void DisableRagdoll()
        {
            foreach (Rigidbody r in rigids)
            {
                r.isKinematic = true; 
            }   
        }
        #endregion
    }
}
