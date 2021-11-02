using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyStunnedState : EnemyState
    {
        float stunTimer;
        public EnemyStunnedState(EnemyContext context) : base(context) { }
        public override void OnStateEnter()
        {   
            context.Health.Damaged = false;
            if(context.Animator != null)
                context.Animator.SetTrigger("Stunned"); 
            context.Agent.NavAgent.isStopped = true;
            stunTimer = context.Health.StunTime; 
        }
        public override void Update()
        {
            base.Update();
            if (stunTimer > 0)
                stunTimer -= Time.deltaTime;
            else
            { 
                context.Agent.NavAgent.isStopped = false;
            }
            
        }

        public override void OnStateExit()
        {  
        }
        public override void Conditions()
        {
            base.Conditions();

                if(stunTimer <= 0)
            {
                if (context.Detect.CurrentState == EnemyDetect.DetectState.Sight ||
                    context.Detect.CurrentState == EnemyDetect.DetectState.InDetectionRange ||
                    context.Detect.CurrentState == EnemyDetect.DetectState.InAttackRange) 
                    context.CurrentState.ChangeState(new EnemyChasePlayerState(context));

                else if (context.Agent.SearchComplete || context.Detect.CurrentState == EnemyDetect.DetectState.Nothing)
                {
                    context.Detect.LookAt(context.Detect.LastHitPosition);
                    context.Agent.SetDestination(context.Detect.LastHitPosition);
                    context.CurrentState.ChangeState(new EnemySearchPlayerState(context));
                }
            }
        }
    } 


}
