using UnityEngine;

namespace BehaviourTree
{
    public class RootNode : Node
    {
        [HideInInspector] public Node childNode;

        public override string nodeName => "Root";
        protected override void OnStart()
        { 

        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
            => childNode.Update();

        public override Node Clone() 
        {
            RootNode root = Instantiate(this);
            root.childNode = childNode.Clone();
            return root;
        }
    }
}
