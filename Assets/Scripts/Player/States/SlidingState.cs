using UnityEngine;

namespace Tzaik.Player
{
    public class SlidingState : State
    { 
        public SlidingState(PlayerController playerContext) : base(playerContext) { }
        public override void OnStateEnter()
        {
            context.Crouch.ChangeHeight(context.Collider.transform.parent, new Vector3(1, context.Crouch.crouchHeight));
            context.MouseLook.IsSliding = true;
            context.Sliding.SlidingEnterEvent.Invoke();
            context.DoTimerCoroutine(context.Sliding.SlidingTime);
        }
        public override void OnStateExit()
        {
            context.Sliding.SlidingExitEvent.Invoke();
            context.Crouch.ChangeHeight(context.Collider.transform.parent, new Vector3(1, context.Crouch.normalHeight));
            context.MouseLook.IsSliding = false;
        }
        public override void Update()
        {
            base.Update();
            context.MouseLook.DoWallRunSideways(false);
            context.Rigidbody.velocity = context.MouseLook.GetCamForward * context.Movement.SprintSpeed*2; 
        }

        public override void Conditions()
        {
            if (context.IsTimerOver)
                context.CurrentState.ChangeState(new CrouchingState(context));
            else if (!context.Checks.IsGrounded())
                context.CurrentState.ChangeState(new FallingState(context, context.Movement.SprintSpeed * 2));
            else if (InputManager.IsJumping)
                context.CurrentState.ChangeState(new JumpingState(context, context.Movement.SprintSpeed * 2));
            else if (!InputManager.IsCrouching)
                context.CurrentState.ChangeState(new IdleState(context));

        }
    }
}