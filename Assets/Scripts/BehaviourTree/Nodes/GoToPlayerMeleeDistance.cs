using UnityEngine;

namespace BehaviourTree
{
    public class GoToPlayerMeleeDistance : ActionNode
    {
        public override string nodeName => "Go To Player Melee distance Position";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.Context.Detect.DistanceBetweenPlayer() <= blackboard.Context.Detect.MeleeDistance)
            {
                blackboard.Context.Agent.NavAgent.SetDestination(blackboard.NextPosition);
                if (blackboard.Context.Agent.PathFinished)
                {
                    return NodeState.Success;
                }
                else
                {
                    return NodeState.Running;
                }
            }
            else
                return NodeState.Running;

        }
    }

}
