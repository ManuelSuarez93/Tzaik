using UnityEngine;

namespace Tzaik.Player
{
    [System.Serializable]
    public class PlayerCrouch
    {
        public float normalHeight = 1;
        public float crouchHeight = 0.5f;
        public float crouchSpeed = 5;
        public float maxDistanceToObjectAbove = 1; 
         

        public void ChangeHeight(Transform colliderParent, Vector3 height) => colliderParent.localScale = height;
        public bool CheckIfNoObjectAbove(Transform t, float maxDistanceToObjectAbove) => Physics.Raycast(t.position, t.up, maxDistanceToObjectAbove);
    }
 
}
