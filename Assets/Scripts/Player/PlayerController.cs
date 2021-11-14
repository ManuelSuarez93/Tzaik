using UnityEngine;
using Tzaik.Player.Cameras;
using Tzaik.Audio;
using System.Collections;
using Tzaik.General;
using Tzaik.Items.Weapons;
using System.Collections.Generic;
using Tzaik.UI;

namespace Tzaik.Player
{ 
    public class PlayerController : MonoBehaviour
    {
        #region Fields  
        [SerializeField] PlayerMovement movement; 
        [SerializeField] PlayerJump jump; 
        [SerializeField] PlayerChecks checks; 
        [SerializeField] PlayerCrouch crouch;
        [SerializeField] PlayerMouseLook mouseLook;
        [SerializeField] HeadBobbing headBob; 
        [SerializeField] PlayerDashing dashing;
        [SerializeField] PlayerWallRun walRun;
        [SerializeField] PlayerSliding sliding;
        [SerializeField] PlayerInventory inventory;
        [SerializeField] PlayerInteract interact;
        [SerializeField] bool UseCharacterController; 
        [SerializeField] CapsuleCollider collider;
        [SerializeField] PlayerSpecial special;
        [SerializeField] HealthScript health; 

        bool isTimerOver;
        FootstepAudio footstep;
        State currentState;
        public bool DebugEnable; 
        Vector3 velocity;
        Rigidbody rigidBody;
        #endregion

        #region Properties
        public State CurrentState { get => currentState; set => currentState = value; }
        public CapsuleCollider Collider { get => collider; set => collider = value; }
        public Vector3 Velocity { get => velocity; set => velocity = value; }
        public Rigidbody Rigidbody { get => rigidBody; set => rigidBody = value; }
        public FootstepAudio Footstep { get => footstep; set => footstep = value; }
        public PlayerWallRun WallRun => walRun;
        public PlayerMovement Movement => movement;
        public PlayerDashing Dashing => dashing;
        public PlayerJump Jump => jump; 
        public PlayerMouseLook MouseLook => mouseLook;
        public PlayerSliding Sliding => sliding;
        public PlayerChecks Checks => checks; 
        public HeadBobbing HeadBob => headBob;
        public PlayerCrouch Crouch => crouch;
        public PlayerInventory Inventory => inventory;
        public PlayerInteract Interact => interact;
        public PlayerSpecial Special => special;
        public HealthScript Health => health;
        public bool IsTimerOver => isTimerOver;  

        #endregion

        #region Unity Methods
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.useGravity = false;
            Movement.Rigid = rigidBody;
            footstep = GetComponentInChildren<FootstepAudio>();
            InputManager.Initialize(this);
        }
        private void Start()
        {
            StartCoroutine(CanInteract());
            currentState = new IdleState(this);
        }

        private void Update()
        {
            currentState.Update();
            if (DebugEnable) { Debugging(); }
             
        }
         
        private void FixedUpdate()
            => currentState.FixedUpdate(); 
        #endregion

        public void Debugging()
        {
            Debug.Log($"<color=green>Current state:</color> <color=cyan>{CurrentState}</color>");
            GameManager.Instance.StateText($"<color=green>Current state:</color> <color=cyan>{CurrentState}</color>");
        }
        public void ChangeState(State newState) => currentState.ChangeState(newState);
        public void PlayerMove(float speed)
            => Movement.RigidMovement(InputManager.Movement.ReadValue<Vector2>().x, 
                                        InputManager.Movement.ReadValue<Vector2>().y, 
                                        speed, MouseLook.GetCamForward, MouseLook.GetCamRight, checks.IsGrounded());
        #region Coroutines
        public void DoTimerCoroutine(float time)
            => StartCoroutine(TimerCoroutine(time));
        public void DoCoroutine(string routine)
        {
            StopAllCoroutines();
            StartCoroutine(routine);
        }

        public IEnumerator CanInteract()
        {
            while (true)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, mouseLook.GetCamForward, out hit, checks.MaxDistanceToDetectObject);
                if (hit.collider != null)
                {
                    if (Vector3.Distance(transform.position, hit.collider.transform.position) > 5f)
                        UIManager.Instance.Crosshair.color = Color.blue;
                    else if (hit.collider.tag == "Enemy/Controller")
                        UIManager.Instance.Crosshair.color = Color.red;
                }
                else
                {
                    UIManager.Instance.Crosshair.color = Color.green;
                }
                yield return new WaitForSeconds(0.1f);
            }

        }
        IEnumerator TimerCoroutine(float time)
        {
            isTimerOver = false; 
            var timer = time;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            } 
            isTimerOver = true;
            yield return null;
        } 
        IEnumerator SidewaysLeftCoroutine()
        {
            Debug.Log("SidewaysLeft CoRoutine being called");
            while(mouseLook.ZRotation <= mouseLook.ZRotationMax)
            {
                mouseLook.SidewaysLeft();
                yield return null;
            }
            mouseLook.ZRotation = mouseLook.ZRotationMax;
        } 
        IEnumerator SidewaysRightCoroutine()
        {
            Debug.Log("SidewaysRight CoRoutine being called");
            while (mouseLook.ZRotation >= -mouseLook.ZRotationMax)
            {
                mouseLook.SidewaysRight();
                yield return null;
            }
            mouseLook.ZRotation = -mouseLook.ZRotationMax;
        } 
        IEnumerator CenterCameraFromLeft()
        {

            Debug.Log("CenterCameraFromLeft CoRoutine being called");
            while (mouseLook.ZRotation > mouseLook.SidewaysTolerance)
            {
                mouseLook.CenterCamera();
                yield return null;
            }
            mouseLook.ZRotation = 0;
        } 
        IEnumerator CenterCameraFromRight()
        {
            Debug.Log("CenterCameraFromRight CoRoutine being called");
            while (mouseLook.ZRotation < -mouseLook.SidewaysTolerance)
            {
                mouseLook.CenterCamera();
                yield return null;
            }
            mouseLook.ZRotation = 0;
        }
        #endregion

    }


}