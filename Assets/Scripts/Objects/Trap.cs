using System.Collections;
using UnityEngine;

namespace Tzaik
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] float detectionDistance;
        [SerializeField] float moveObjectAmount;
        [SerializeField] float moveRate;
        [SerializeField] Transform trapObject;
        [SerializeField] LayerMask detectionLayer;

        void Start() 
            => StartCoroutine(DetectRoutine());
        IEnumerator DetectRoutine()
        { 
            while(true)
            {
                Debug.DrawLine(transform.position, transform.up);
                if (Detect())
                    SetTrap();

                yield return new WaitForSeconds(0.1f); 
            }
        }

        private bool Detect() => Physics.Raycast(transform.position,transform.up, detectionDistance);
        private void SetTrap() => trapObject.position =
            Vector3.Lerp(trapObject.position,
                trapObject.position + (Vector3.up * moveObjectAmount), 
                Time.deltaTime * moveRate);
    }
}
