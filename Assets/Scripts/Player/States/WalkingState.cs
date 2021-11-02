using UnityEngine;

namespace Tzaik.Player
{
    public class WalkingState : State
    {
        public WalkingState(PlayerController playerContext) : base(playerContext) { }
        public override void OnStateEnter() 
            => context.HeadBob.CurrentRate = context.HeadBob.WalkSpeed; 
        public override void Update()
        {
            base.Update();
            context.HeadBob.DoHeadBobbing();
            context.PlayerMove(context.Movement.Speed);

            if (context.Footstep.CurrentTag != context.Checks.GroundTag)
                context.Footstep.ChangeTag(context.Checks.GroundTag); 
        }

        public override void Conditions()
        {
            if (!InputManager.IsMoving)
                context.CurrentState.ChangeState(new IdleState(context));
            else if (InputManager.IsJumping)
                context.CurrentState.ChangeState(new JumpingState(context, context.Movement.Speed));
            else if (InputManager.IsSprinting)
                context.CurrentState.ChangeState(new SprintState(context));
            else if (InputManager.IsCrouching)
                context.CurrentState.ChangeState(new CrouchingState(context));
            else if (!context.Checks.IsGrounded())
                context.CurrentState.ChangeState(new FallingState(context, context.Movement.Speed));
            else if (InputManager.IsDashing && InputManager.IsLeft)
                context.CurrentState.ChangeState(new DashingState(context, false));
            else if (InputManager.IsDashing && InputManager.IsRight)
                context.CurrentState.ChangeState(new DashingState(context, true));
        }
    }
}