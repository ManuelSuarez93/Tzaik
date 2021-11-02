namespace Tzaik.Player
{
    public class DashingState: State
    {
        bool isRight;
        public DashingState(PlayerController playerContext, bool IsRight) : base(playerContext) { isRight = IsRight; }

        public override void OnStateEnter()
        {
            context.Movement.RestrictDownwards =  
                context.Movement.RestrictForwards =  
                context.Movement.RestrictSideways = true;
            context.Dashing.DashEnterEvent.Invoke();
            context.DoTimerCoroutine(context.Dashing.DashTime); 
        }
        public override void Update()
        {
            Conditions();
            context.Dashing.Dashing(context.Rigidbody, context.MouseLook.GetCamRight, isRight);
        }
        public override void OnStateExit()
        {
            context.Dashing.DashExitEvent.Invoke();
            context.Movement.RestrictDownwards =  
                context.Movement.RestrictForwards =  
                context.Movement.RestrictSideways = false;
        } 

        public override void Conditions()
        {
            if (context.IsTimerOver)
                context.CurrentState.ChangeState(new IdleState(context));
        }
    }
}