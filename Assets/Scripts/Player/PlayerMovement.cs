using System.Collections;
using UnityEngine;

namespace Tzaik.Player
{ 
    [System.Serializable]
    public class PlayerMovement
    {
        #region Fields
        [SerializeField] float speed = 10, sprintSpeed = 20, gravity = -50f;

        bool restrictSideways, restrictForwards, restrictDownwards;
        Rigidbody rigid;
        #endregion

        #region Properties
        public Rigidbody Rigid { get => rigid; set => rigid = value; }
        public bool RestrictSideways { get => restrictSideways; set => restrictSideways = value; }
        public bool RestrictForwards { get => restrictForwards; set => restrictForwards = value; }
        public bool RestrictDownwards { get => restrictDownwards; set => restrictDownwards = value; }
        public float Speed  => speed;
        public float SprintSpeed => sprintSpeed; 
        #endregion

        public PlayerMovement() { }
        public PlayerMovement(float Speed, float SprintSpeed)
        {
            speed = Speed;
            sprintSpeed = SprintSpeed;
        }

        #region Methods
        public void RigidMovement(float sideways, float forwards,float speed, Vector3 CamForward, Vector3 CamRight, bool IsGrounded)
        { 
            var Sideways = (!restrictSideways ? CamRight * sideways : Vector3.zero) * speed;
            var Forwards = (!restrictForwards ? CamForward * forwards : Vector3.zero) * speed;
            var downwards = !restrictDownwards ? new Vector3(0, rigid.velocity.y) : Vector3.zero;
            rigid.velocity = Sideways + Forwards + downwards;
        }

        public void Gravity() => rigid.AddForce(Vector3.up * gravity);

        #endregion
    }
}
