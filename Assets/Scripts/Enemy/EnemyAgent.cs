using System.Collections;
using Tzaik.Enemy;
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
        [SerializeField] float distanceThreshold;
        float timer;

        public float RemainigDistance;
        public Vector3 NextPosition;
        #endregion

        #region Properties
        public NavMeshAgent NavAgent { get => navAgent; set => navAgent = value; }
        public bool SearchComplete { get; private set; }
        public bool IsSideways { get; set; }
        public float TimeForSearch => timeForSearch;
        public float ForwardVelocity { get; set; }
        public float RightVelocity { get; set; }
        public float LeftVelocity { get; set; }
        #endregion

        #region Unity Methods
        void Awake()
            => NavAgent = GetComponent<NavMeshAgent>();
        private void Update()
        {
            CalculateVelocities();
            DebugNavAgent();
        }

        public void DebugNavAgent()
        {
            NextPosition = NavAgent.destination;
            RemainigDistance = NavAgent.remainingDistance; 
        }
        #endregion

        #region Methods
        public bool SetDestination(Vector3 d, float distance, Transform objective)
        {
            var distanceObj = Vector3.Distance(transform.position, objective.position);
            var max = distance + distanceThreshold;
            var min = distance - distanceThreshold;
            if (distanceObj >= max)
            {
                NavAgent.SetDestination(d); 
                return false;
            }
            else if (distanceObj <= max && distanceObj >= min)
            {
                NavAgent.SetDestination(transform.position); 
                return true;
            }
            else if (distanceObj < min)
            {
                NavAgent.SetDestination(SetDirection() * distanceObj); 
                return false;
            }

            return false;
        }

        public void GetNewDestination(Vector3 position, float distance)
        { 
            RaycastHit hit;
            Vector3 direction = transform.forward * -1;
            var rand = Random.Range(-1, 2);
            if (!Physics.Raycast(transform.position, transform.right * rand, out hit, 5f))
                if (hit.transform == null) direction = transform.right * rand;

            NavAgent.SetDestination(position + direction * distance);
            IsSideways = true;
        } 
        public Vector3 SetDirection()
        {
            RaycastHit hit;
            Vector3 direction = transform.forward;
            if (Physics.Raycast(transform.position, transform.forward * -1, out hit, 5f))
                if (hit.transform == null) direction = transform.forward * -1;
            if (Physics.Raycast(transform.position, transform.right * -1, out hit, 5f))
                if (hit.transform == null) direction = transform.right * -1;
            if (Physics.Raycast(transform.position, transform.right, out hit, 5f))
                if (hit.transform == null) direction = transform.right;

            return direction;
        }


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
        public bool PathFinished => NavAgent.remainingDistance <= distanceThreshold;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, navAgent.destination);
        }
        #endregion
    }
     
}
