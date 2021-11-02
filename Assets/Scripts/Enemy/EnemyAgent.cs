using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Tzaik
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAgent : MonoBehaviour
    {
        #region Fields
        [SerializeField] float timeForSearch;
        [SerializeField] NavMeshAgent navAgent;
        float timer;
        #endregion

        #region Properties
        public NavMeshAgent NavAgent { get => navAgent;  set => navAgent = value; }
        public bool SearchComplete { get; private set; }
        public float TimeForSearch => timeForSearch;  
        public float ForwardVelocity { get; set; }
        public float RightVelocity { get; set; }
        public float LeftVelocity { get; set; }
        #endregion

        #region Unity Methods
        void Awake()
            =>  NavAgent = GetComponent<NavMeshAgent>();
        private void Update()
        {
            CalculateVelocities();
        }
        #endregion

        #region Methods
        public void SetDestination(Vector3 d)
            => NavAgent.SetDestination(d);
          
        public void CalculateVelocities() 
        {
            Vector3 normalizedMovement = navAgent.desiredVelocity.normalized;
            Vector3 forwardVector = Vector3.Project(normalizedMovement, transform.forward); 
            Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);
            Vector3 leftVector = Vector3.Project(normalizedMovement, transform.right * -1);

            ForwardVelocity = forwardVector.magnitude * Vector3.Dot(forwardVector, transform.forward); 
            RightVelocity = rightVector.magnitude * Vector3.Dot(rightVector, transform.right);
            LeftVelocity = leftVector.magnitude * Vector3.Dot(rightVector, transform.right * -1);
        }
        public float CalculateForward()
        { 
            var angle = Vector3.Angle(NavAgent.velocity.normalized, transform.forward);
            if (NavAgent.velocity.normalized.x < transform.forward.x)
                angle *= -1;
            angle = (angle + 180.0f) % 360.0f;

            return angle;
        }
        public bool PathFinished() => NavAgent.pathStatus == NavMeshPathStatus.PathInvalid 
            || NavAgent.pathStatus == NavMeshPathStatus.PathPartial 
            || NavAgent.remainingDistance <= 0 || !NavAgent.pathPending;
        public void DoSearch()
            => StartCoroutine(Search()); 
        IEnumerator Search()
        {
            SearchComplete = false;
            timer = timeForSearch;
            while(timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            SearchComplete = true;
        }


        #endregion
    }
     
}
