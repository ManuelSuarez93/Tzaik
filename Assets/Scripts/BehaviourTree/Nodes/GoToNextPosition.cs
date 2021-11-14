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
                blackboard.Context.Agent.NavAgent.SetDestination(Random.insideUnitSphere + blackboard.NextPosition);
                return NodeState.Success;
        }
    }

}
