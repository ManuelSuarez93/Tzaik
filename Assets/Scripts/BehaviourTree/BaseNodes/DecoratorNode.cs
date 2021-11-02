using UnityEngine;

namespace BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node childNode;
         

        public override Node Clone()
        {
            DecoratorNode decorator = Instantiate(this);
            decorator.childNode = childNode.Clone();
            return decorator;
        }
    }
     
}
