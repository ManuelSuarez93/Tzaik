using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(EnemyContext context) : base(context) { }
        public override void OnStateEnter()
        {
            context.Animator.SetBool("Attacking", true);
            context.Animator.SetBool("Moving", false);
            context.Mesh.material.color = Color.red;
            context.Attack.Objective = context.Detect.playerTransform; 
            context.Sounds.SetActive(true);
        }
        public override void OnStateExit()
        {
            context.Animator.SetBool("Attacking", false);
            context.Sounds.SetActive(false);
        }
        public override void Update()
        {
            base.Update();
            context.Detect.LookAt(context.Detect.playerTransform.position);
        }

        public override void Conditions()
        {
            base.Conditions();

            if (context.Detect.CurrentState == EnemyDetect.DetectState.InDetectionRange ||
                context.Detect.CurrentState == EnemyDetect.DetectState.Nothing)
                context.CurrentState.ChangeState(new EnemySearchPlayerState(context));

            else if (context.Detect.DistanceBetweenPlayer() > context.Detect.AttackRadius)
                context.CurrentState.ChangeState(new EnemyChasePlayerState(context));
        }
    } 


}
