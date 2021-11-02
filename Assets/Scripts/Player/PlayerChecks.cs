using UnityEngine;

namespace Tzaik.Player
{
    [System.Serializable]
    public class PlayerChecks
    {
        [Header("CameraTransform")]
        [SerializeField] Transform cam, player;
        [Header("Ground check")]
        [SerializeField] float maxDistanceToGround;
        [SerializeField] float maxSlopeAngle;
        [Header("Wall check")]
        [SerializeField] float maxDistanceToWall;
        [Header("Interactable check")]
        [SerializeField] float maxDistanceToDetectObject;
        [Header("Debugging")]
        [SerializeField] bool EnableDebugging;

        string groundTag;
        public string GroundTag => groundTag;
        public float MaxDistanceToDetectObject => maxDistanceToDetectObject;
        public bool IsGrounded()
        {
            RaycastHit hit;

            var grounded = Physics.Raycast(player.position, player.up * -1,out hit, maxDistanceToGround); 

            if(grounded)
            {   
                if (Vector3.Angle(Vector3.up, hit.normal) < Mathf.Abs(maxSlopeAngle))
                {
                    return true;
                }
            }

            groundTag = hit.collider != null ? hit.collider.tag : "" ;
            return grounded; 
        }
        public bool CanWallRunRight() => Physics.Raycast(player.position, cam.right, maxDistanceToWall);
        public bool CanWallRunLeft() => Physics.Raycast(player.position, cam.right * -1, maxDistanceToWall);
        public IInteractable GetInteractable()
        {
            RaycastHit hit;
            Physics.Raycast(player.position, cam.forward,out hit, maxDistanceToDetectObject);
            if(hit.collider != null) 
                return hit.collider.gameObject.GetComponent<IInteractable>(); 

            return null;
        }

        



    }
}
