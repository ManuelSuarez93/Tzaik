using UnityEngine;

namespace BehaviourTree
{
    public class GoToDistancePlayerPosition : ActionNode
    {
        public override string nodeName => "Go To Player Ranged Position";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.Context.Agent.SetDestination(blackboard.NextPosition,
                blackboard.Context.Detect.AttackRadius, blackboard.Context.Detect.playerTransform))
                return NodeState.Success;
            else
                return NodeState.Running;
        }
    }


}
