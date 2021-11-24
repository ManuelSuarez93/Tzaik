namespace BehaviourTree
{
    public class TriggerNewAnimation : ActionNode
    {
        public string triggerName; 
        public override string nodeName => "Trigger animation";
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            //blackboard.Context.Animator.SetTrigger(triggerName);
            return NodeState.Success;
        }
    }

}
