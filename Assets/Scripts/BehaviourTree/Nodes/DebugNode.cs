using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class DebugNode : ActionNode
    {
        [SerializeField] string message;
        public override string nodeName => "Debug"; 

        public string Message { get => message; set => message = value; }
        protected override void OnStart() => Debug.Log($"On Start{message}");
        protected override void OnStop() => Debug.Log($"On Stop{message}");
        protected override NodeState OnUpdate()
        {
            Debug.Log($"On Update{message}");
            return NodeState.Success;
        }
    }
}
