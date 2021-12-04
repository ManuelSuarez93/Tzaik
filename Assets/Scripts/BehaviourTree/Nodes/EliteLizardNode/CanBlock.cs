using Tzaik.Enemy;

namespace BehaviourTree
{
    public class CanBlock : DecoratorNode
    {
        public override string nodeName => "Can block?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            var el = blackboard.Context.Attack as EliteLizardAttack;
            if (!el.BlockingPerformed)
            {
                childNode.Update();
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
