using UnityEngine;

namespace Tzaik.Player
{
    public class HurtState : State
    {
        float timer;
        public HurtState(PlayerController PlayerContext) : base(PlayerContext) { }
        public override void OnStateEnter()
        {
            timer = Time.time;
            context.Rigidbody.velocity = Vector3.zero; 
            context.Rigidbody.AddForce(context.Health.ForceRecieved, context.Health.ForceTypeReceived);
            context.Health.SetStunned();
        }
        public override void OnStateExit()
        {
            context.Health.ForceRecieved = Vector3.zero;
            context.Health.Damaged = false; 
        }

        public override void Update()
        {
            if (timer + context.Health.StunTime < Time.time)  
                context.CurrentState.ChangeState(new IdleState(context)); 

            
            context.MouseLook.MouseLooking();
        }
    }
}