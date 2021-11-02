using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyChasePlayerState: EnemyState
    {
        public EnemyChasePlayerState(EnemyContext context) : base(context) { }
        public override void OnStateEnter()
        {
            context.Animator.SetBool("Moving", true);
            context.Mesh.material.color = Color.magenta;
            context.Sounds.SetActive(true);
        }
        public override void OnStateExit()
        {
            context.Sounds.SetActive(false);
        }
        public override void Update()
        {
            base.Update();
            if (context.Detect.playerTransform != null)
            { 
                context.Agent.SetDestination(context.Detect.playerTransform.position); 
                context.Detect.LookAt(context.Detect.playerTransform.position);
            }
        }

        public override void Conditions()
        {
            base.Conditions();
            if (context.Detect.CurrentState == EnemyDetect.DetectState.InDetectionRange ||
                context.Detect.CurrentState == EnemyDetect.DetectState.Nothing || context.Detect.playerTransform == null) 
                context.CurrentState.ChangeState(new EnemySearchPlayerState(context)); 
            else if (context.Detect.CurrentState == EnemyDetect.DetectState.InAttackRange) 
                context.CurrentState.ChangeState(new EnemyAttackState(context)); 
        }
    }


}
