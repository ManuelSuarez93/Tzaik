using UnityEngine;

namespace Tzaik.Player
{
    public class HurtState : State
    {
        public HurtState(PlayerController PlayerContext) : base(PlayerContext)   { }
        public override void OnStateEnter()
        {
            context.Rigidbody.velocity = Vector3.zero;
            context.Rigidbody.AddForce(context.Health.ForceRecieved, context.Health.ForceTypeReceived);
            context.CurrentState.ChangeState(new IdleState(context));
        }
        public override void OnStateExit()
        {
            context.Health.ForceRecieved = Vector3.zero;
            context.Health.Damaged = false;
        }

        public override void Update() => context.MouseLook.MouseLooking();

    }
}