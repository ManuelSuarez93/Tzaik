using UnityEngine;

namespace BehaviourTree
{
    public class IsInMeleeAttackDistance : DecoratorNode
    { 
        public override string nodeName => "Is Objective in melee attack distance?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.CurrentDetectState == Tzaik.Enemy.EnemyDetect.DetectState.InAttackRangeMelee)
            {
                childNode.Update();
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
