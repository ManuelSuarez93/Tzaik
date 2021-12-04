using UnityEngine;

namespace BehaviourTree
{
    public class PerformAttackMelee : ActionNode
    {
        public override string nodeName => "Perform attack melee";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.NextPosition != null)
            { 
                blackboard.Context.Attack.PerformMeleeAttack();
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        }
    }
}
