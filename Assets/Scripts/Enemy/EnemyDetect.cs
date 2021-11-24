using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyDetect : MonoBehaviour
    {
        #region Fields 
        [SerializeField] float rangeRadius =3f;
        [SerializeField] float sightRadius = 3f;
        [SerializeField] float attackRadius = 1f;
        [SerializeField] float meleeDistance = 1f;
        [SerializeField] float lookAtSmoothing = 1f; 
        [SerializeField] Animator anim;
        [SerializeField] List<string> tags;
        [Tooltip("Time inbetween detections")]
        [SerializeField] float detectionTimeRate = 0.1f;
        [SerializeField] float angleDetection = 90f;
        [SerializeField] DetectState currentState;
        [Tooltip("Layer of player")]
        [SerializeField] LayerMask playerLayer;
        [Header("Debuging")]
        [SerializeField] bool enableDebug = false;
        Vector3 lastHitPosition;
        
        #endregion


        public enum DetectState { Nothing = 0, InDetectionRange = 1, Sight = 2, InAttackRange = 3, InAttackRangeMelee = 4 }
        #region Properties
        public Transform playerTransform { get; private set; }
        public float AttackRadius  => attackRadius; 
        public DetectState CurrentState => currentState; 
        public Vector3 LastHitPosition => lastHitPosition; 
        public float MeleeDistance => meleeDistance;  
        #endregion

        #region Unity Methods
        void Start()
        {
            StartCoroutine(DetectRoutine()); 
        }
        #endregion

        #region Meothds
        void GetPlayer()
        {   
            var a = Physics.OverlapSphere(transform.position, rangeRadius, playerLayer);
            playerTransform = a.Length > 0 ? a.ToList().FirstOrDefault().transform : null;
        }

        private bool HasLineOfSight(float range)
        {  
            RaycastHit hit;
            if (Physics.Raycast(transform.position, DirectionToPlayer(), out hit, range, playerLayer)) 
                if (hit.collider.transform == playerTransform) return true; 
                

            return false;  
        }
          
        IEnumerator DetectRoutine()
        {
            while(true)
            {
                GetPlayer();
                if (playerTransform == null)
                {
                    currentState = DetectState.Nothing;
                }
                else
                {
                    currentState = DetectState.InDetectionRange;
                    if (InFieldOfView() && HasLineOfSight(sightRadius))
                    {
                        currentState = DetectState.Sight; 
                        {
                            if (DistanceBetweenPlayer() < attackRadius)
                            {
                                currentState = DetectState.InAttackRange;
                                if (DistanceBetweenPlayer() <= meleeDistance)
                                    currentState = DetectState.InAttackRangeMelee;
                            }
                        }
                    }
                    
                }
                yield return new WaitForSeconds(detectionTimeRate);
            }
        }

        private void OnDrawGizmos()
        {
            if (enableDebug)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, rangeRadius);
                Ray ray = new Ray(transform.position, DirectionToPlayer());
                Gizmos.DrawRay(ray);
            }
        }
        public void LookAt(Vector3 objective)
            => transform.forward = Vector3.Lerp(transform.forward, objective - transform.position, lookAtSmoothing * Time.deltaTime);
        public void LookAtPlayer()
            => transform.forward = Vector3.Lerp(transform.forward, playerTransform.position - transform.position, lookAtSmoothing * Time.deltaTime);
        public float DistanceBetweenPlayer() => Vector3.Distance(transform.position, playerTransform.position); 
        private bool InFieldOfView() => playerTransform != null ? Vector3.Angle(transform.forward, DirectionToPlayer()) < angleDetection : false;
        public Vector3 DirectionToPlayer() => playerTransform != null ? (playerTransform.position - transform.position).normalized : transform.forward;
        private void OnCollisionEnter(Collision collision)
        {
            if (tags.Contains(collision.gameObject.tag))
                lastHitPosition = collision.contacts.FirstOrDefault().point;
        }
        #endregion

    }

}
