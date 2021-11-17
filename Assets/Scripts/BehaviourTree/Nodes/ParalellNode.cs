using UnityEngine;

namespace BehaviourTree
{ 
    public class ParalellNode : CompositeNode
    {
        int current;
        public override string nodeName => "Paralell";
        protected override void OnStart() => current = 0;

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        { 
            foreach(Node child in childNodes)
            {
                switch (child.Update())
                {
                    case NodeState.Running:
                        break;
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
