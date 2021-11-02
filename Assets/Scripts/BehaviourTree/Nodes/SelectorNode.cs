using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class SelectorNode : CompositeNode
    {
        public override string nodeName => "Selector";
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override NodeState OnUpdate()
        {
            foreach(Node child in childNodes)
                switch (child.Update())
                {
                    case NodeState.Running:
                        return NodeState.Running;
                    case NodeState.Success:
                        return NodeState.Success; 
                    case NodeState.Failure:
                        state = NodeState.Failure;
                        break;
                }

            return state;
        }
    }
}
