using UnityEngine;

namespace BehaviourTree
{ 
    public class WhileNode : CompositeNode
    {
        int current;
        public override string nodeName => "While Node";
        protected override void OnStart() => current = 0;

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        { 
            if(childNodes[0].Update() == NodeState.Running) 
                return NodeState.Running; 
            else if(childNodes[0].Update() == NodeState.Failure) 
                return NodeState.Failure; 
            else
            {
                for (int i = 1;  i < childNodes.Count; i++)
                switch (childNodes[i].Update())
                {
                    case NodeState.Running:
                        return NodeState.Running;
                    case NodeState.Success:
                        current++;
                        break;
                    case NodeState.Failure:
                        return NodeState.Failure;
                }
            }

            return current == childNodes.Count ? NodeState.Success : NodeState.Running;
        }
    }
}
