using UnityEngine;

namespace BehaviourTree
{ 
    public class IsDeadNode : ActionNode
    {
        public override string nodeName => "Is Dead?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.CurrentHealth <= 0)
            {
                blackboard.Context.Health.DeathEvent.Invoke();
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        }
    }
}
