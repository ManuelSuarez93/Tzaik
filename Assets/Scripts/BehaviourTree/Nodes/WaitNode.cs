using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class WaitNode : ActionNode
    {
        public float duration = 1; 
        float startTime; 
        public override string nodeName => "Wait";
        protected override void OnStart() => startTime = Time.time;

        protected override void OnStop()
        { 
        }

        protected override NodeState OnUpdate()
        { 
            if (Time.time - startTime > duration)
            {
                return NodeState.Success; 
            } 

            return NodeState.Running;
        }
    }
}
