using Tzaik.Enemy;

namespace BehaviourTree
{
    public class CanSummon : DecoratorNode
    {
        public override string nodeName => "Can perform summon?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        { 
            var summon = blackboard.Context.Attack as EliteZombieAttack;
            if (summon.CanSummon)
            {
                childNode.Update();
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
