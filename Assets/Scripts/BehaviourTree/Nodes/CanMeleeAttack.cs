namespace BehaviourTree
{
    public class CanMeleeAttack : DecoratorNode
    {
        public override string nodeName => "Can attack melee?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.CanMelee)
            {
                childNode.Update();
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
