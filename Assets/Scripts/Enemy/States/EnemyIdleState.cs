using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(EnemyContext context) : base(context) { }
        public override void OnStateEnter()
        {
            context.Animator.SetBool("Moving", false);
            context.Mesh.material.color = Color.green;
        }
        
        public override void Conditions()
        {
            base.Conditions();
            if (context.Detect.CurrentState == EnemyDetect.DetectState.Sight)
            {
                context.CurrentState.ChangeState(new EnemyChasePlayerState(context));
            }
            else if (context.Detect.CurrentState == EnemyDetect.DetectState.InAttackRange)
            {
                context.CurrentState.ChangeState(new EnemyAttackState(context));
            }
        }


    }


}
