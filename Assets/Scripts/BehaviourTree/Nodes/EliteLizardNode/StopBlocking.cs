using Tzaik.Enemy;

namespace BehaviourTree
{
    public class StopBlocking : ActionNode
    {
        public override string nodeName => "Stop blocking";
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
                attack.PerformBlock(false);
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
