using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class GoToDistancePlayerPosition : ActionNode
    {
        public override string nodeName => "Go To Player Ranged Position";
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
                if (blackboard.Context.Detect.DistanceBetweenPlayer() >= blackboard.Context.Detect.AttackRadius)
                {
                    blackboard.Context.Agent.SetDestination(blackboard.NextPosition);
                    return NodeState.Running;
                }
                else if(blackboard.Context.Detect.DistanceBetweenPlayer() <= blackboard.Context.Detect.AttackRadius - 5f)
                { 
                    blackboard.Context.Agent.SetDestination(blackboard.Context.transform.forward * -2);
                    return NodeState.Running;
                }
                else if (blackboard.Context.Detect.DistanceBetweenPlayer() >= blackboard.Context.Detect.AttackRadius - 5f)  
                {
                    blackboard.Context.Agent.SetDestination(blackboard.Context.transform.position);
                    return NodeState.Success;
                } 
            } 

            return NodeState.Failure;
        }
    }


}
