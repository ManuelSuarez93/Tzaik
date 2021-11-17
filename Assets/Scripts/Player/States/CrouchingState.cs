using UnityEngine;
namespace Tzaik.Player
{
    public class CrouchingState : State
    {
        public CrouchingState(PlayerController playerContext) : base(playerContext) { }
        public override void OnStateEnter()
        {
            context.HeadBob.Crouching = true;
            context.Crouch.ChangeHeight(context.Collider.transform.parent, new Vector3(1,context.Crouch.crouchHeight));
        }
        public override void OnStateExit()
        {
            context.HeadBob.Crouching = false;
            context.Crouch.ChangeHeight(context.Collider.transform.parent, new Vector3(1, context.Crouch.normalHeight)); 
        }
        public override void Update()
        {
            base.Update();

            if(Mathf.Abs(context.Rigidbody.velocity.x) > 0 || Mathf.Abs(context.Rigidbody.velocity.y) > 0) 
                context.HeadBob.DoHeadBobbing();
             
        }

        public override void FixedUpdate() => context.PlayerMove(context.Crouch.crouchSpeed);

        public override void Conditions()
        {
            if (!context.Crouch.CheckIfNoObjectAbove(context.transform, context.Crouch.maxDistanceToObjectAbove))
            {
                if (!InputManager.IsCrouching)
                    context.CurrentState.ChangeState(new IdleState(context));
                else if (InputManager.IsJumping && context.Checks.IsGrounded()) 
                    context.CurrentState.ChangeState(new JumpingState(context, context.Movement.Speed)); 
            }

            else if (!context.Checks.IsGrounded()) 
                context.CurrentState.ChangeState(new FallingState(context, context.Movement.Speed)); 

            else if(context.Health.Damaged) 
                context.CurrentState.ChangeState(new HurtState(context));
        }
    }
}