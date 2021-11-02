using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemySearchPlayerState: EnemyState
    {
        public EnemySearchPlayerState(EnemyContext context) : base(context) { }
        public override void OnStateEnter()
        { 
            context.Mesh.material.color = Color.yellow;
            context.Agent.DoSearch();
        }
        public override void OnStateExit()
            => context.Agent.NavAgent.SetDestination(context.transform.position);
        public override void Update()
        {
            base.Update();
            if (Vector3.Distance(context.Agent.NavAgent.destination, context.transform.position) 
                < context.Agent.NavAgent.stoppingDistance) 
                context.Agent.SetDestination(context.transform.position + Random.insideUnitSphere * 10);  
            context.Animator.SetBool("Moving", Mathf.Abs(context.Agent.NavAgent.velocity.magnitude) > 0.1f);
        }
        public override void Conditions()
        {
            base.Conditions();
            if (context.Agent.SearchComplete)
            {
                if (context.Agent.PathFinished() || context.Detect.CurrentState == EnemyDetect.DetectState.Nothing)
                    context.CurrentState.ChangeState(new EnemyIdleState(context)); 
            } 

            if (context.Detect.CurrentState == EnemyDetect.DetectState.Sight)
                context.CurrentState.ChangeState(new EnemyChasePlayerState(context));
            
        }
    }


}
