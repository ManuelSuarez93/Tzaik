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
            if (blackboard.Context.Detect.DistanceBetweenPlayer() >= blackboard.Context.Detect.MeleeDistance)
            {
                blackboard.Context.Agent.SetDestination(blackboard.NextPosition, blackboard.Context.Detect.MeleeDistance, 
                    blackboard.Context.Detect.playerTransform);
                return NodeState.Success;
            }  
            else
            {
                return NodeState.Running;
            } 
        }
    }

}
