using Tzaik.Enemy;

namespace BehaviourTree
{
    public class StartBlocking : ActionNode
    {
        public override string nodeName => "Start blocking";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            var attack = blackboard.Context.Attack as EliteLizardAttack;
            if (!attack.IsBlocking)
            {
                attack.PerformBlock(true);
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
