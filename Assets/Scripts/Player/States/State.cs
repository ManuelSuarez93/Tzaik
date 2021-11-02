namespace Tzaik.Player
{
    public abstract class State
    {
        protected PlayerController context;
        public State(PlayerController PlayerContext)
        {
            context = PlayerContext;
        }

        public virtual void OnStateExit() { }
        public virtual void OnStateEnter() { }
        public void ChangeState(State state)
        {
            context.CurrentState.OnStateExit();
            context.CurrentState = state;
            context.CurrentState.OnStateEnter();
        }
        public virtual void FixedUpdate() => context.Movement.Gravity();
        public virtual void Conditions() { }  
        
        public virtual void Update() 
        {
            context.MouseLook.MouseLooking();
            context.MouseLook.Zoom();
            //context.MouseLook.SidewaysLeft(); 
            Conditions();
        } 
    }
 
}