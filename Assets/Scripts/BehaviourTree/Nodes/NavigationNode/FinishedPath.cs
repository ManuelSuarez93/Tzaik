namespace BehaviourTree
{
    public class FinishedPath : DecoratorNode
    {
        public override string nodeName => "Has finished Path?";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (blackboard.Context.Agent.NavAgent.remainingDistance <= 1f)
            {
                childNode.Update();
                return NodeState.Running;
            }

            return NodeState.Failure;
        }
    }
}
