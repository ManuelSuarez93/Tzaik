using Tzaik.Enemy;

namespace BehaviourTree
{
    public class PerformSummon : ActionNode
    {
        public override string nodeName => "Perform summon";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.NextPosition != null)
            {
                var a = blackboard.Context.Attack as EliteZombieAttack;
                a.PerformSummon();
                return NodeState.Success;
            }
            else
                return NodeState.Failure;
        }
    }
}
