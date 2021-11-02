namespace Tzaik.Enemy
{
    public class EnemyDeadState : EnemyState
    {
        public EnemyDeadState(EnemyContext context) : base(context) { }
        public override void OnStateEnter()
        {    
            context.Health.DeathEvent.Invoke();  
        }

        public override void Conditions()
        { 
        }
    }


}
