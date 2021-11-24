using UnityEngine;

namespace Tzaik.Enemy
{
    public class EnemyAnimator: MonoBehaviour
    {
        [SerializeField] Animator animator;

        public void SetAnimator(Animator a) => animator = animator ??= a;
        public void SetTrigger(string trigger) => animator.SetTrigger(trigger);
        public void SetBool(string name, bool value) => animator.SetBool(name, value);
        public void Animations(EnemyAgent agent)
        {
            animator.SetFloat("SpeedX", agent.ForwardVelocity);
            animator.SetFloat("SpeedY", Mathf.InverseLerp(-0.99f, 1, agent.RightVelocity));
            animator.SetFloat("SpeedYLeft", Mathf.InverseLerp(-0.99f, 1, agent.LeftVelocity));
            animator.SetFloat("Rotation", agent.CalculateForward());
        }
    }
}
