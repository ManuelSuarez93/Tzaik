using UnityEngine;

namespace BehaviourTree
{ 
    public class RepeatNode : DecoratorNode
    {

        public override string nodeName => "Repeat";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            childNode.Update(); 
            return NodeState.Running;
        }
    }
}
