using UnityEngine;

namespace BehaviourTree
{ 
    public class PerformAttackRanged : ActionNode
    {
        public override string nodeName => "Perform attack ranged";
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
                blackboard.Context.AttackConditions.PerformRange();
                blackboard.Context.Attack.PerformRagnedAttack();
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        }
    }

}
