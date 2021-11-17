using UnityEngine;

namespace BehaviourTree
{ 
    public class WaitAndCheckNodes : DecoratorNode
    {
        public float duration = 1;
        float startTime;
        public override string nodeName => "Wait & Check";
        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (Time.time - startTime > duration)
            {
                childNode.Update();
                return NodeState.Success;
            }

            return NodeState.Running;
        }
    }

}
