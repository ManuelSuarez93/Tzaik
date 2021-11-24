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
            if(blackboard.Context.Agent.IsSideways)
            {
                if (blackboard.Context.Agent.NavAgent.remainingDistance <= 1f)
                {
                    blackboard.Context.Agent.IsSideways = false;
                    return NodeState.Success;
                }
                else
                    return NodeState.Running;
            }
            else
            { 
                blackboard.Context.Agent.GetNewDestination(blackboard.CurrentPosition, blackboard.Context.Detect.MeleeDistance); 
                return NodeState.Running;
            }
        }
    }
     
}
