namespace BehaviourTree
{
    public class GoToPlayerRangedDistance : ActionNode
    {
        public override string nodeName => "Go To Player Ranged distance Position";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.Context.Detect.DistanceBetweenPlayer() <= blackboard.Context.Detect.AttackRadius)
            {
                blackboard.Context.Agent.NavAgent.SetDestination(blackboard.NextPosition);
                if (blackboard.Context.Agent.NavAgent.remainingDistance <= 1f)
                    return NodeState.Success;
                else
                    return NodeState.Running;
            }
            else
                return NodeState.Running;

        }
    }
}
