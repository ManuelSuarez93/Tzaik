using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class GoToNextPosition : ActionNode
    {
        public override string nodeName => "Go To Next Position";
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
                blackboard.Context.Agent.SetDestination(Random.insideUnitSphere + blackboard.NextPosition);
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        }
    }

}
