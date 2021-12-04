using UnityEngine;

namespace BehaviourTree
{ 
    public class SequenceNode : CompositeNode
    {
        int current;
        public override string nodeName => "Sequence";
        protected override void OnStart() => current = 0;

        protected override void OnStop()
        { 
        }

        protected override NodeState OnUpdate()
        {
            var child = childNodes[current];
            switch(child.Update())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    current++;
                    break;
                case NodeState.Failure:
                    return NodeState.Failure;
            }

            return current == childNodes.Count ? NodeState.Success : NodeState.Running;
        }
    }
}
