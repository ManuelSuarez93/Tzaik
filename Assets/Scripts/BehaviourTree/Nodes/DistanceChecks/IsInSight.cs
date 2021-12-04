using UnityEngine;

namespace BehaviourTree
{ 
    public class IsInSight : DecoratorNode
    {

        public override string nodeName => "Is Objective in sight?";
        protected override void OnStart()
        { 
        }

        protected override void OnStop()
        { 
        }

        protected override NodeState OnUpdate()
        {
            if ((int)blackboard.CurrentDetectState >= (int)Tzaik.Enemy.EnemyDetect.DetectState.Sight)
            {
                childNode.Update();
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        } 
    }
}
