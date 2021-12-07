using UnityEngine;

namespace Tzaik.Player
{
    public class FallingState : State
    {
        float currentSpeed;
        public FallingState(PlayerController playerContext, float Speed) : base(playerContext) { currentSpeed = Speed; }
        public override void OnStateExit() 
            => context.Velocity = Vector3.zero;
        public override void Update()
        {
            base.Update();
            if (currentSpeed > context.Movement.Speed)
                currentSpeed -= Time.deltaTime * 0.25f; 
        }
        public override void FixedUpdate()
        {
            context.Movement.Gravity();
            context.PlayerMove(currentSpeed);
        }

        public override void Conditions()
        {
            if (context.Checks.IsGrounded())
            {
                context.Jump.ResetJump();
                context.CurrentState.ChangeState(new IdleState(context));
            }  
            else if (InputManager.IsJumping)
                context.CurrentState.ChangeState(new JumpingState(context, context.Movement.Speed));
            else if (context.Checks.CanWallRunLeft() && InputManager.IsLeft)
               context.CurrentState.ChangeState(new WallRunState(context, false));
            else if (context.Checks.CanWallRunRight() && InputManager.IsRight)
               context.CurrentState.ChangeState(new WallRunState(context, true));
        }
    }
}