namespace Tzaik.Enemy
{
    public abstract class EnemyState
    {
        protected EnemyContext context;
        public EnemyState(EnemyContext PlayerContext)
        {
            context = PlayerContext;
        } 
        public virtual void OnStateExit() { }
        public virtual void OnStateEnter() { }
        public void ChangeState(EnemyState state)
        {
            context.CurrentState.OnStateExit();
            context.CurrentState = state;
            context.CurrentState.OnStateEnter();
        }
        public virtual void Update() { Conditions(); }

        public virtual void Conditions()
        {
            if (context.Health.Damaged)
                context.CurrentState.ChangeState(new EnemyStunnedState(context));
            if (context.Health.CurrentHealth <= 0)
                context.CurrentState.ChangeState(new EnemyDeadState(context));
        }
    }


}
