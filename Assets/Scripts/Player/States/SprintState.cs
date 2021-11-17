using UnityEngine;

namespace Tzaik.Player
{
    public class SprintState : State
    {
        public SprintState(PlayerController playerContext) : base(playerContext) { }

        public override void OnStateEnter() => 
            context.HeadBob.CurrentRate = context.HeadBob.SprintSpeed;  
        
        
        public override void Update()
        {
            base.Update();
            context.HeadBob.DoHeadBobbing();

            if (context.Footstep.CurrentTag != context.Checks.GroundTag)
                context.Footstep.ChangeTag(context.Checks.GroundTag);
             
        }
        public override void FixedUpdate() => context.PlayerMove(context.Movement.SprintSpeed);
        public override void Conditions()
        {
            if (context.Rigidbody.velocity == Vector3.zero)
                context.CurrentState.ChangeState(new IdleState(context));
            else if (!context.Checks.IsGrounded())
                context.CurrentState.ChangeState(new FallingState(context, context.Movement.SprintSpeed));
            else if (!InputManager.IsSprinting)
                context.CurrentState.ChangeState(new WalkingState(context));
            else if (InputManager.IsJumping && context.Checks.IsGrounded())
                context.CurrentState.ChangeState(new JumpingState(context, context.Movement.SprintSpeed));
            //else if (InputManager.IsCrouching)
            //    context.CurrentState.ChangeState(new SlidingState(context));
            else if (InputManager.IsDashing && InputManager.IsLeft)
                context.CurrentState.ChangeState(new DashingState(context, false));
            else if (InputManager.IsDashing && InputManager.IsRight)
                context.CurrentState.ChangeState(new DashingState(context, true));
            else if (context.Health.Damaged)
                context.CurrentState.ChangeState(new HurtState(context));
        }
    }
}