using UnityEngine;

namespace BehaviourTree
{ 
    public class IsStunned : DecoratorNode
    {
        public override string nodeName => "Is enemy stunned?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.isStunned)
            {
                return childNode.Update();
            }

            return NodeState.Failure;
        }
    }
}
