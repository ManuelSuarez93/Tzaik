using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class IsInRange : DecoratorNode
    {
        public override string nodeName => "Is Objective in range?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if ((int)blackboard.CurrentDetectState >= (int)Tzaik.Enemy.EnemyDetect.DetectState.InDetectionRange)
            {
                childNode.Update();
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        }
    } 
}
