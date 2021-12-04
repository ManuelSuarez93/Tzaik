using UnityEngine;

namespace BehaviourTree
{
    public class PerfomTimeAction : DecoratorNode
    {
        public float duration = 1;
        public float coolDownDuration = 5;
        float startTime;
        float coolDown;
        bool inCoolDown;

        public override string nodeName => "Perform timed action";
        protected override void OnStart()
        {
            inCoolDown = false;
        }

        protected override void OnStop()
        { 
        }

        protected override NodeState OnUpdate() 
        {  
            if(!inCoolDown)
            {
                startTime = Time.time;
                if (Time.time - startTime > duration)
                {
                    if (childNode.Update() == NodeState.Running)
                        return NodeState.Running;
                    else
                    { 
                        startTime = Time.time;
                        coolDown = Time.time + coolDownDuration;
                        inCoolDown = false;
                        return NodeState.Failure;
                    }
                }
            }
            else
            {
                if(Time.time - coolDown > coolDownDuration)
                {
                    startTime = Time.time; 
                    inCoolDown = false;
                }

                return NodeState.Failure; 
            }

            return NodeState.Failure; 
        }
    }
}
