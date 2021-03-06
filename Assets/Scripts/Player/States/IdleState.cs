namespace Tzaik.Player
{
    public class IdleState : State
    {
        public IdleState(PlayerController playerContext) : base(playerContext)   {  }
 
        public override void Conditions()
        { 
            if(InputManager.IsMoving)
                context.CurrentState.ChangeState(new WalkingState(context));
            else if (InputManager.IsJumping)
                context.CurrentState.ChangeState(new JumpingState(context, context.Movement.Speed));
            else if (!context.Checks.IsGrounded())
                context.CurrentState.ChangeState(new FallingState(context, context.Movement.Speed)); 
            else if (context.Health.Damaged)
                context.CurrentState.ChangeState(new HurtState(context));
        }
    }
}

