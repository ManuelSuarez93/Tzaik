using Tzaik.Enemy;

namespace BehaviourTree
{
    public class IsBlocking : DecoratorNode
    {
        public override string nodeName => "Is Blocking?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            var attack = blackboard.Context.Attack as EliteLizardAttack;
            if (attack.IsBlocking)
            {
                childNode.Update();
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
