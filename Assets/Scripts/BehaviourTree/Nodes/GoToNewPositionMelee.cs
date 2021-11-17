namespace BehaviourTree
{
    public class GoToNewPositionMelee : ActionNode
    {
        public override string nodeName => "Go To new position";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            blackboard.Context.Agent.GetNewDestination(blackboard.Context.Detect.MeleeDistance);
            if (!blackboard.Context.Agent.NavAgent.pathPending)
                return NodeState.Success;
            else
                return NodeState.Running;
        }
    }

}
