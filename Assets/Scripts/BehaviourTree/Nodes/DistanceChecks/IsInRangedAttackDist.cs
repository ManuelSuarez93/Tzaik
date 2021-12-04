namespace BehaviourTree
{
    public class IsInRangedAttackDist : DecoratorNode
    {
        public override string nodeName => "Is Objective in ranged attack distance?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.CurrentDetectState == Tzaik.Enemy.EnemyDetect.DetectState.InAttackRange)
            {
                childNode.Update();
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        }
    }
}
