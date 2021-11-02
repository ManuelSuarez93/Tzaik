using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class IsInAttackDistance : DecoratorNode
    {
        public override string nodeName => "Is Objective in attack distance?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if ((int)blackboard.CurrentDetectState >= (int)Tzaik.Enemy.EnemyDetect.DetectState.InAttackRange)
            {
                childNode.Update();
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        }
    }
}
