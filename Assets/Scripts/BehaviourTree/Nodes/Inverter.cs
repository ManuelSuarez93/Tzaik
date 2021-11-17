using UnityEngine;

namespace BehaviourTree
{ 
    public class Inverter : DecoratorNode
    {
        public override string nodeName => "Inverter";
        protected override void OnStart()
        { 
        }

        protected override void OnStop()
        { 
        }

        protected override NodeState OnUpdate()
        {
            switch(childNode.Update())
            {
                case NodeState.Success:
                    return NodeState.Failure;
                case NodeState.Failure:
                    return NodeState.Success;
                case NodeState.Running:
                    return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }


}
