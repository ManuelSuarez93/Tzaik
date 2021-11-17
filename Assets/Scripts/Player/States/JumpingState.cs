using UnityEngine;

namespace Tzaik.Player
{
    public class JumpingState : State
    {
        float movingSpeed;
        
        public JumpingState(PlayerController playerContext, float MovingSpeed) :
            base(playerContext) { movingSpeed = MovingSpeed; }

        public override void OnStateEnter() => context.Jump.Jumping(context.Rigidbody); 
        public override void FixedUpdate()
        {
            context.PlayerMove(movingSpeed);
            context.Movement.Gravity();
        }
        public override void Conditions()
        { 
            //if (context.Checks.CanWallRunLeft() && InputManager.IsLeft)
            //    context.CurrentState.ChangeState(new WallRunState(context, false));
            //else if (context.Checks.CanWallRunRight() && InputManager.IsRight)
            //    context.CurrentState.ChangeState(new WallRunState(context, true));
            if (context.Rigidbody.velocity.y < context.Jump.JumpHeight || !context.Checks.IsGrounded())
                context.CurrentState.ChangeState(new FallingState(context, context.Movement.Speed));
            else if (InputManager.IsDashing && InputManager.IsLeft)
                context.CurrentState.ChangeState(new DashingState(context, false));
            else if (InputManager.IsDashing && InputManager.IsRight)
                context.CurrentState.ChangeState(new DashingState(context, true)); 
            else if (context.Health.Damaged)
                context.CurrentState.ChangeState(new HurtState(context));
        }
    }
}