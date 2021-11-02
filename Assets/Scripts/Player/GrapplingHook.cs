using System.Collections;
using System.Collections.Generic;
using Tzaik.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Tzaik.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class GrapplingHook : MonoBehaviour
    {
        
        [SerializeField] Transform cam, player, guntip; 
        [SerializeField] float minDistance;
        [SerializeField] float maxDistance;
        [SerializeField] LayerMask grappleLayer;

        LineRenderer line; 
        bool isGrappling;
        Vector3 grapplePoint;
        Vector3 currentGrapplePosition;
        Transform grappleEnemy;

        private void Awake()
        {
            line = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                StartGrapple();
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                StopGrapple();
            }
            
            if(grappleEnemy != null)
            {
                if(Vector3.Distance(player.position, grappleEnemy.position) < minDistance)
                {
                    StopGrapple();  
                }
                else
                { 
                    player.position = Vector3.Lerp( transform.position, grappleEnemy.position, Time.deltaTime * 8);
                }
            }
        }
        private void LateUpdate()
        {
            DrawRope();
        }

        void StartGrapple()
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.position, cam.forward,out hit, maxDistance, grappleLayer))
            { 
                    grappleEnemy = hit.collider.transform;
                    grappleEnemy.GetComponent<NavMeshAgent>().enabled = false;
                    grappleEnemy.GetComponent<EnemyContext>().enabled = false;
                    line.positionCount = 2;
                    currentGrapplePosition = guntip.position;
 
            } 
        }
         
        void DrawRope()
        {
            if (grappleEnemy == null) return;

            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grappleEnemy.position, Time.deltaTime * 8f);
            line.SetPosition(0, guntip.position);
            line.SetPosition(1, currentGrapplePosition);
        }

        void StopGrapple()
        { 
            grappleEnemy = null;
            line.positionCount = 0;  
        }
    }
}
