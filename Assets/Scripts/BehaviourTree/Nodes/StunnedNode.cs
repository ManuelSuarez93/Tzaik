using UnityEngine;
using UnityEngine.Events;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class StunnedNode : ActionNode
    {
        [SerializeField] UnityEvent Event; 
        public override string nodeName => "Set stunned";
         
        protected override void OnStart() { }
        protected override void OnStop() { }
        protected override NodeState OnUpdate()
        {
            blackboard.Context.SetStunned();
            return NodeState.Success;
        }
    }
}
