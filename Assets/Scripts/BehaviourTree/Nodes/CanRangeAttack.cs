namespace BehaviourTree
{
    public class CanRangeAttack : DecoratorNode
    {
        public override string nodeName => "Can attack ranged?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.CanRanged)
            {
                childNode.Update();
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
