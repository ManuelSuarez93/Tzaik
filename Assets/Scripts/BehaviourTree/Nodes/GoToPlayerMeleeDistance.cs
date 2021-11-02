using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
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
            if (blackboard.NextPosition != null)
            {
                blackboard.Context.Agent.NavAgent.isStopped = false;
                if (blackboard.Context.Detect.DistanceBetweenPlayer() >= blackboard.Context.Detect.MeleeDistance)
                {
                    blackboard.Context.Agent.SetDestination(blackboard.NextPosition);
                    return NodeState.Running;
                }
                else
                {
                    blackboard.Context.Agent.SetDestination(blackboard.Context.transform.position);
                    return NodeState.Success;
                }

            }
            else
                return NodeState.Failure;
        }
    }

}
