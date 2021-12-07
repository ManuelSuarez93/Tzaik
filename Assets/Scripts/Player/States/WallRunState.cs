namespace Tzaik.Player
{
    public class WallRunState : State
    {
        bool isRight;
        public WallRunState(PlayerController playerContext, bool IsRight) : base(playerContext) { isRight = IsRight; }

        public override void OnStateEnter()
        {
            context.Movement.RestrictDownwards = true; 
            context.HeadBob.CurrentRate = context.HeadBob.SprintSpeed;
            context.WallRun.WallRunEnterEvent.Invoke();
            context.DoTimerCoroutine(context.WallRun.WallrunTime);
        }
        public override void OnStateExit()
        {
            context.WallRun.WallRunExitEvent.Invoke();
            context.Movement.RestrictDownwards = false; 
            if(isRight)
                context.DoCoroutine("CenterCameraFromLeft");
            else
                context.DoCoroutine("CenterCameraFromRight");
        }
        public override void Update()
        {
            context.MouseLook.MouseLooking(); 
            Conditions();
            context.HeadBob.DoHeadBobbing();
            context.MouseLook.DoWallRunSideways(isRight);
            context.PlayerMove(context.Movement.SprintSpeed);

            if (context.Footstep.CurrentTag != context.Checks.GroundTag)
                context.Footstep.ChangeTag(context.Checks.GroundTag);
             
        }

        public override void Conditions()
        {
            if (isRight)
            {
                if (!context.Checks.CanWallRunRight() || InputManager.IsLeft)
                { context.CurrentState.ChangeState(new FallingState(context, context.Movement.SprintSpeed)); }
            }
            else
            {
                if (!context.Checks.CanWallRunLeft() || InputManager.IsRight)
                { context.CurrentState.ChangeState(new FallingState(context, context.Movement.SprintSpeed)); }
            } 
            if (InputManager.IsJumping)
                context.CurrentState.ChangeState(new JumpingState(context, context.Movement.SprintSpeed));
            if (context.IsTimerOver)
                context.CurrentState.ChangeState(new FallingState(context, context.Movement.SprintSpeed));

            else if (InputManager.IsJumping)
                context.CurrentState.ChangeState(new FallingState(context, context.Movement.SprintSpeed));
        }
    }
}